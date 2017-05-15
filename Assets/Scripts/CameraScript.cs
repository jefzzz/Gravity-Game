using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public int gameMode = 0; //0, play as ship. 1, play as planet
    public float cameraHeight = 1000;
    public float minimumHeight = 500;
    public float maximumHeight = 9000;
    public Transform target;
    private Vector3 currentCameraVelocity;
    public float trackingSpeed = 0.1f;
    public float zoomLevel;

    private float refZoomLevel;
	// Use this for initialization
	void Start () {
        zoomLevel = cameraHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void LateUpdate()
    {
        zoomLevel += Input.GetAxis("Mouse ScrollWheel") * -1000;
        zoomLevel = Mathf.Clamp(zoomLevel, minimumHeight, maximumHeight);
        //Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 targetPos = new Vector3(target.position.x, zoomLevel, target.position.z);
        //targetPos = targetPos + Vector3.ClampMagnitude(target.GetComponent<Rigidbody>().velocity, 100);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentCameraVelocity, trackingSpeed);
        //Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoomLevel, ref refZoomLevel, 0.1f);
    }
}
    