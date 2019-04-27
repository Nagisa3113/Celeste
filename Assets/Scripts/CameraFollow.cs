using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform pos1;//摄像机位置
    public Transform pos2;

    Vector3 offset;//偏移
    Transform playerPosition;//玩家位置
    Vector3 cameraVelocity = Vector3.zero;//初始跟随速度

    // Use this for initialization
    void Start()
    {
        //offset = transform.position - playerPosition.transform.position;
        transform.position = pos1.position;

        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        if (playerPosition.position.x > 14.5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, pos2.position, ref cameraVelocity, 0.3f);
        }
        if (playerPosition.position.x < 14.5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, pos1.position, ref cameraVelocity, 0.3f);
        }

        //offset.y = -playerPosition.transform.position.y;
        //transform.position = Vector3.SmoothDamp(transform.position, playerPosition.transform.position + offset, ref cameraVelocity, 0.1f);
    }
}
