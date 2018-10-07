using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour {

    public int choice = 1;
    public Transform pos1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if(choice==1&&Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(1);
        }
    }
}
