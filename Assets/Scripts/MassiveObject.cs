using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveObject : MonoBehaviour {
    [Range(0.0001f,100)]
    public float mass = 1;
    [Range(1, 50)]
    public float radius = 1;
    [Range(1, 5000)]
    public float gravitationalRadius = 100;
    [Range(1, 500)]
    public float slingshotRadius = 1;
    [Range(0, 10)]
    public float slingshotStrength = 0;

    public Collider trigger;
    public Collider physical;

    private TrailRenderer trail;
    // Use this for initialization
    void Start()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, .4f, .6f, 0.9f, 1f);
        trail = GetComponent<TrailRenderer>();
        transform.FindChild("scale/planet").GetComponent<MeshRenderer>().material.color = randomColor;
        trail.startColor = randomColor;
        randomColor.a = 0;
        trail.endColor = randomColor;
        float trailWidth = Mathf.Clamp(radius / 2, 0.5f, 2);
        trail.startWidth = trailWidth;
        trail.endWidth = trailWidth;

        if (trigger && physical)
        {
            Physics.IgnoreCollision(trigger, physical);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(10))
        {
            print("Planet to Planet collision!");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer.Equals(11))
        {
            print("Planet to Star collision!");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
