using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float fuelAmount = 1;
    public float fuelConsumptionRate = 0.1f;
    public GameObject fuelBar;

    public bool isInfluenced = false;
    private Rigidbody rb;
    public bool isRotating = false;
    public float thrustForce = 25;
    public float velMag = 0;
    public float interPlanetarySpeedLimit = 500;
    public float hardLimit = 1000;
    public float decelerationRate = 100; // units per second per second

    public ParticleSystem engineParticles;
    private Vector3 refVel;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5000, ForceMode.Acceleration);
	}
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        velMag = rb.velocity.magnitude;
        if (Input.GetMouseButton(0))
        {
            fuelAmount -= fuelConsumptionRate * Time.deltaTime;
            fuelAmount = Mathf.Clamp(fuelAmount, 0, 1);
            fuelBar.transform.localScale = new Vector3(fuelAmount, 1, 1);
            Rotate();
            if (fuelAmount > 0)
            {
                Thrust();
                engineParticles.Play();
            }
            else
            {
                engineParticles.Stop();
            }
        }
        else
        {
            engineParticles.Stop();
        }
        if (!isInfluenced) //if player is in interplanetary space, speed is limited.
        {
            //SmoothClampVelocity(interPlanetarySpeedLimit, 0.5f);
        }
    }
    private void Rotate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = targetRotation;
        }
    }
    private void Thrust()
    {
        rb.AddForce(transform.forward * thrustForce, ForceMode.Acceleration);
    }
    private void SmoothClampVelocity(float target, float smoothTime)
    {
        Vector3 clampedVel = Vector3.ClampMagnitude(rb.velocity, interPlanetarySpeedLimit);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, clampedVel, ref refVel, smoothTime);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, hardLimit);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(10))
        {
            isInfluenced = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(10))
        {
            isInfluenced = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Reset();
    }
    private void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        fuelAmount = 1;
    }
}