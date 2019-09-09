using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public Transform[] cameraPosList;//摄像机位置
    public Transform curCameraPos;

    public Transform[] ArchivePosList;
    public Transform curArchivePos;//存档点记录

    Vector3 cameraVelocity = Vector3.zero;//初始跟随速度

    private void Awake()
    {
        curArchivePos = ArchivePosList[0];
        curCameraPos = cameraPosList[0];

    }

    private void LateUpdate()
    {

    }

    public void SceneChange(int index)
    {

    }

    public void CameraFollow(int index)
    {
        curCameraPos = cameraPosList[index];

        transform.position = Vector3.SmoothDamp
            (transform.position, curCameraPos.position, ref cameraVelocity, 0.3f);
    }


    public void ArchivePosChange(int index)
    {
        curArchivePos = ArchivePosList[index];
    }


}
