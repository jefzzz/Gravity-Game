using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShowInfluence : MonoBehaviour {
    public bool isGR;
    public bool isSR;
    private Planet planet;
    private float gR;
    private float sR;
	// Use this for initialization
	void Start () {
        displayInfluence();
    }
	
	// Update is called once per frame
	void Update () {
        displayInfluence();
    }
    void displayInfluence()
    {
        planet = transform.root.GetComponent<Planet>();
        if (isGR)
        {            
            gR = planet.gravitationalRadius;
            transform.localScale = new Vector3(gR, transform.localScale.y, gR);
        }
        if (isSR)
        {
            sR = planet.slingshotRadius;
            transform.localScale = new Vector3(sR, transform.localScale.y, sR);
        }
    }
}
