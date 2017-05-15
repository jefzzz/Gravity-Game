using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [Range(1,10000)]
    public float mass = 100;
    [Range(1, 1000)]
    public float gravitationalRadius = 80;
    [Range(1, 1000)]
    public float slingshotRadius = 20;
    [Range(0, 10)]
    public float slingshotStrength = 1;
	// Use this for initialization
	void Start () {
        transform.FindChild("scale/planet").GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, .4f, .6f, 0.9f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
	}
    private void OnTriggerStay(Collider other)
    {
        
    }
}
