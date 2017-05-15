using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour {
    public int mode = 1;
    ParticleSystem particles;
    public float speed = 300;
    public float scale = 1;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (mode == 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (mode == 1)
        {
            scale = 1 + Time.fixedTime * speed * (scale * 1.0000001f);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
