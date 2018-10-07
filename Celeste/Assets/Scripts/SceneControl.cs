using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

    public GameObject player;
    public GameObject dashGem;
    public GameObject stone;
    public float curTime1;
    public float curTime2;
    public float curTime3;

    public Transform pos1;//存档点位置
    public Transform pos2;

    public float rePlayer = 0.5f;
    public float reGemTime = 2;
    public float reStone = 2;

	// Use this for initialization
	void Start () {
        player.transform.position =pos1.position;

    }

    // Update is called once per frame
    void Update () {
        if (dashGem.activeSelf == false)
        {
            curTime1 += Time.deltaTime;
        }

        if (curTime1 >=reGemTime)
        {
            dashGem.SetActive(true);
            curTime1 = 0;
        }

        if (stone.activeSelf == false)
        {
            curTime2 += Time.deltaTime;
        }

        if (curTime1 >= reStone)
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
            player.SetActive(true);
            if (transform.position.x < 14)
                player.transform.position = pos1.position;
            if (transform.position.x > 14)
                player.transform.position = pos2.position;
            curTime3 = 0;
        }



    }
}
