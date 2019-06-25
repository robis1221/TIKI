using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour {

    CharacterController cc;
    AudioSource s_source;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        s_source = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if(cc.isGrounded == true && cc.velocity.magnitude > 2f && s_source.isPlaying == false)
        {
            s_source.volume = Random.Range(0.3f, 0.5f);
            s_source.pitch = Random.Range(0.6f, 1);
            s_source.Play();
        }
		
	}
}
