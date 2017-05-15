using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ScrollingBackground : MonoBehaviour {
    public Transform target;
    public Camera c;
    public float bgOffset = -500;
	// Use this for initialization
	void Start () {
        c = Camera.main;
        target = c.transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = new Vector3(target.position.x, bgOffset, target.position.z);
        transform.position = targetPos;
        float s = (c.transform.position.y - transform.position.y) * 0.577f;
        s = s / 10;
        transform.localScale = new Vector3(s, s, s);
    }
}
