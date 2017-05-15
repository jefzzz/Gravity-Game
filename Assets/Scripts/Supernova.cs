using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour {
    public int mode = 1;
    ParticleSystem particles;
    public float speed = 5;
    [Range(0.01f,0.1f)]
    public float acceleration = 1;
    public float size = 10;
    public float scale = 1;
    public float t = 0;
    public bool isDetonated = false;
    public GameObject starMesh;
    // Use this for initialization
    void Start () {
        transform.localScale = Vector3.zero;
        GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || isDetonated == true)
        { 
            isDetonated = true;
            starMesh.SetActive(false);
            GetComponent<SphereCollider>().enabled = true;
            if (mode == 0)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (mode == 1)
            {
                t += Time.fixedDeltaTime;
                scale = size * t / (t + 1 / acceleration);
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}
