using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public GameObject stone;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") 
        {
               Destroy(stone, 1);

        }
    }
}
