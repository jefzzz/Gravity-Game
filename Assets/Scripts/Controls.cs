using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float velMag = 0;
    public Color playerColor = new Color(1,1,1,0.5f);

    public float startVel = 0;
    public float initialFuel = 100; //total deltaV
    public float remainingFuel;
    public float fuelConsumptionRate = 10; //rate of deltaV

    public GameObject fuelBar;

    private Rigidbody rb;
    public ParticleSystem engineParticles;
    public Light engineLight;
    public TrailRenderer trail;
    private Vector3 refVel;

    private bool isInfluenced = false;

    //junk
    private float interPlanetarySpeedLimit = 500;
    private float hardLimit = 1000;
    private float decelerationRate = 100; // units per second per second
    //junk
    
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startVel, ForceMode.VelocityChange);
        //fuel stuff
        remainingFuel = initialFuel;

        //pretty stuff
        ParticleSystem.MainModule settings = engineParticles.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(playerColor);
        engineLight.color = playerColor;
        playerColor.a = 1;
        trail.startColor = playerColor;
        playerColor.a = 0;
        trail.endColor = playerColor;
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        velMag = rb.velocity.magnitude;

        fuelBar.transform.localScale = new Vector3(remainingFuel/initialFuel, 1, 1);
        remainingFuel = Mathf.Clamp(remainingFuel, 0, int.MaxValue);
        if (Input.GetMouseButton(0))
        {
            Rotate();
            if (remainingFuel > 0)
            {
                Thrust();
                remainingFuel -= fuelConsumptionRate * Time.fixedDeltaTime;
                engineParticles.Play();
                engineLight.enabled = true;
            }
            else
            {
                engineParticles.Stop();
                engineLight.enabled = false;
            }
        }
        else
        {
            engineParticles.Stop();
            engineLight.enabled = false;
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
        rb.AddForce(transform.forward * fuelConsumptionRate * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    private void SmoothClampVelocity(float target, float smoothTime)
    {
        Vector3 clampedVel = Vector3.ClampMagnitude(rb.velocity, interPlanetarySpeedLimit);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, clampedVel, ref refVel, smoothTime);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, hardLimit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(12))
        {
            Reset();
        }
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
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        remainingFuel = initialFuel;
        trail.Clear();
        rb.velocity = Vector3.zero;
    }
}