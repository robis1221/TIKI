using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailMateHead : MonoBehaviour {

    public Transform player;
    public GameObject dialogue;
    private bool turned=false;
	
	// Update is called once per frame
	void Update () {
        if (dialogue.GetComponent<linearDialog>().active)
        {
            transform.LookAt(player);
            turned = true;
        }
        if(turned)
        {
            Destroy(this);
        }
	}
}
