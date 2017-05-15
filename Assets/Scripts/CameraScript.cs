using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float cameraHeight = 100;
    public Transform target;
    private Vector3 currentCameraVelocity;
    public float trackingSpeed = 0.1f;
    public float zoomLevel;
	// Use this for initialization
	void Start () {
        zoomLevel = cameraHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void LateUpdate()
    {
        zoomLevel += Input.GetAxis("Mouse ScrollWheel") * -200;
        Vector3 targetPos = new Vector3(target.position.x, zoomLevel, target.position.z);
        //targetPos = targetPos + Vector3.ClampMagnitude(target.GetComponent<Rigidbody>().velocity, 100);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentCameraVelocity, trackingSpeed);
    }
}
    