using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGem : MonoBehaviour {

    public Player player;
    public GameObject dashGem;

    public float timer = 0;

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
            player.canDash = true;
            dashGem.SetActive(false);
        }
    }
}
