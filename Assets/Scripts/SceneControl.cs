using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{

    public GameObject player;
    public GameObject dashGem;
    public GameObject stone;
    public float curTime1;
    public float curTime2;
    public float curTime3;
    public Transform archivePos;//存档点记录
    public Transform pos1;//存档点1
    public Transform pos2;//存档点2

    public float rePlayer = 0.5f;
    public float reGemTime = 2;
    public float reStone = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dashGem.activeSelf == false)
        {
            curTime1 += Time.deltaTime;
        }

        if (curTime1 >= reGemTime)
        {
            dashGem.SetActive(true);
            curTime1 = 0;
        }

        if (stone.activeSelf == false)
        {
            curTime2 += Time.deltaTime;
        }

        if (curTime2 >= reStone)
        {
            stone.SetActive(true);
            curTime2 = 0;
        }

        if (player.activeSelf == false)
        {
            curTime3 += Time.deltaTime;
        }

        if (curTime3 >= rePlayer)
        {
            if (player.transform.position.x < 14)
                archivePos.position = pos1.position;
            if (player.transform.position.x > 14)
                archivePos.position = pos2.position;
            player.transform.position = archivePos.position;
            curTime3 = 0;
            player.SetActive(true);

        }



    }
}
