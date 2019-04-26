using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public GameObject stone;
    public float timeCounter;
    public bool coll;

    // Use this for initialization
    void Start () {
        timeCounter = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(coll)
            timeCounter -= Time.deltaTime;

        if (timeCounter <= 0)
        {
            stone.SetActive(false);
            coll = false;
            timeCounter = 1f;
        }
          
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            coll = true;
       

    }
}
