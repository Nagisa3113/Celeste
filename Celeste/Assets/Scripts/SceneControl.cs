using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {


    public GameObject dashGem;
    public float curTime;
    public float reGemTime = 2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (dashGem.activeSelf == false)
        {
            curTime += Time.deltaTime;
        }

        if (curTime >= 2f)
        {
            dashGem.SetActive(true);
            curTime = 0;
        }
    }
}
