using UnityEngine;

public class Gravity : MonoBehaviour {
    private Rigidbody rb;
    [Range(0,100)]
    public float G = 1f;
    public Transform orbitalParent;
    public bool startOrbit = true;
    public Vector3 orbitalVelocity;
    // Use this for initialization
    void Start () {
        G = FindObjectOfType<GravityManager>().gravitationalConstant;
        G = G * 10000;
        rb = GetComponent<Rigidbody>();
        if (GetComponent<MassiveObject>())
        {
            rb.mass = GetComponent<MassiveObject>().mass;
        }
        if (startOrbit && orbitalParent)
        {
            /*
            float r = Vector3.Distance(transform.position, orbitalParent.position);
            Vector3 tangent = Vector3.Normalize(Vector3.Cross(transform.position - orbitalParent.position, transform.up));
            float m = 0;
            if (transform.GetComponent<MassiveObject>())
            {
                m = transform.GetComponent<MassiveObject>().mass;
            }
            orbitalVelocity = tangent * Mathf.Sqrt((G * orbitalParent.GetComponent<MassiveObject>().mass + m) / r);
            rb.AddForce(orbitalVelocity, ForceMode.VelocityChange);
            if (orbitalParent.GetComponent<Gravity>().orbitalParent)
            {
                Vector3 orbitalVelocity2 = orbitalParent.GetComponent<Gravity>().orbitalVelocity;
                rb.AddForce(orbitalVelocity2, ForceMode.VelocityChange);
                Debug.DrawRay(rb.worldCenterOfMass, orbitalVelocity2, Color.cyan, 5);
            }
            Debug.DrawRay(rb.worldCenterOfMass, orbitalVelocity, Color.red, 5);
            */
            Vector3 orbitalVelocity = calculateRequiredOrbitalVelocity(transform, orbitalParent);
            rb.AddForce(orbitalVelocity, ForceMode.VelocityChange);
            Debug.DrawRay(rb.worldCenterOfMass, orbitalVelocity, Color.red, 5);
        }
    }
	public Vector3 calculateRequiredOrbitalVelocity(Transform a, Transform b)
    {
        float r = Vector3.Distance(a.position, b.position);
        Vector3 tangent = Vector3.Normalize(Vector3.Cross(a.position - b.position, transform.up));
        float m = 0;
        if (a.GetComponent<MassiveObject>())
        {
            m = a.GetComponent<MassiveObject>().mass;
        }
        orbitalVelocity = tangent * Mathf.Sqrt((G * b.GetComponent<MassiveObject>().mass + m) / r);
        if (b.GetComponent<Gravity>().orbitalParent)
        {
            orbitalVelocity += calculateRequiredOrbitalVelocity(b, b.GetComponent<Gravity>().orbitalParent);
        }
        return orbitalVelocity;
    }
	// Update is called once per frame
	void Update () {
        //Debug.DrawRay(rb.worldCenterOfMass, rb.velocity / 4, Color.cyan);
    }
    private void OnTriggerStay(Collider other)
    {
        MassiveObject otherMassiveObject = other.transform.root.GetComponent<MassiveObject>();
        //get the offset between the objects
        Vector3 offset = Vector3.zero;
        if (otherMassiveObject)
        {
            //print(gameObject.name + " is inside " + otherMassiveObject.name + "'s " + other.gameObject.name);
            offset = otherMassiveObject.transform.position - rb.worldCenterOfMass;
            float sqrmag = offset.sqrMagnitude;
            //check distance is more than 0 (to avoid division by 0) and then apply a gravitational force to the object
            if (sqrmag > 0.0001f)
            {
                float attraction = rb.mass * otherMassiveObject.mass * G;
                rb.AddForce(attraction * offset.normalized / sqrmag);
                Debug.DrawRay(rb.worldCenterOfMass, Mathf.Clamp(attraction,  100000, int.MaxValue) * offset.normalized / sqrmag, Color.green);
            }
        }
    }
}
