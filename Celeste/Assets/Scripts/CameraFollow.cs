using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public Transform playerPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 off;
        off.x = playerPosition.position.x;
        off.y = playerPosition.position.y;
        off.z = transform.position.z;
        transform.position = off;
	}
}
