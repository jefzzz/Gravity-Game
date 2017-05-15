using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanets : MonoBehaviour {
    public GameObject planet;
    public int x;
    public int y;
    public float scaleX = 20;
    public float scaleY = 20;
    public float maxOffset = 10;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 pos = new Vector3((i * scaleX ) + Random.Range(-maxOffset, maxOffset), 0, (j * scaleY) + Random.Range(-maxOffset, maxOffset));
                pos = transform.position + pos;
                GameObject planetClone = Instantiate(planet, pos, Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
