using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class OrientShadow : MonoBehaviour {
    public Transform centralStar;
	// Use this for initialization
	void Start () {
        transform.LookAt(centralStar);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(centralStar);
	}
}
