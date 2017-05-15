using UnityEngine;

public class Gravity : MonoBehaviour {
    private Rigidbody rb;
    [Range(0,500)]
    public float G = 100f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawRay(rb.worldCenterOfMass, rb.velocity / 4, Color.cyan);
    }
    private void OnTriggerStay(Collider other)
    {
        Planet planet = other.transform.root.GetComponent<Planet>();
        //get the offset between the objects
        Vector3 offset = planet.transform.position - rb.worldCenterOfMass;


        //get the squared distance between the objects
        float sqrmag = offset.sqrMagnitude + 10;
        //check distance is more than 0 (to avoid division by 0) and then apply a gravitational force to the object
        if (sqrmag > 0.0001f)
        {
            float attraction = rb.mass * planet.mass * G;
            rb.AddForce(attraction * offset.normalized / sqrmag, ForceMode.Acceleration);
            Debug.DrawRay(rb.worldCenterOfMass, 5 * attraction * offset.normalized / sqrmag, Color.green);

            
        }
        float mag = offset.magnitude;
        if (mag < planet.slingshotRadius)
        {
            float boost = Mathf.Clamp(planet.slingshotStrength / sqrmag, 0, 0.01f);
            rb.velocity *= 1 + boost;
        }
    }
}
