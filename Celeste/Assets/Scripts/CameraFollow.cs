using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset;
    public Transform playerPosition;
    public Vector3 cameraVelocity = Vector3.zero;
	// Use this for initialization
	void Start () {
        offset = transform.position - playerPosition.transform.position;
    }

    // Update is called once per frame
    void Update () {
        offset.y = -playerPosition.transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition.transform.position + offset, ref cameraVelocity, 0.1f);
	} 
}
