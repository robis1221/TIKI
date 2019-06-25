using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall : MonoBehaviour {
    public GameObject guiElement;
    public AudioSource sound;
    public float startOfTheCall;
    public GameObject dialogueObj;
    private float sceneLoadTime;
    private bool[] started= { false, false };
	// Use this for initialization
	void Start () {
        sceneLoadTime = Time.time;
        guiElement.SetActive(false);
        sound.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > sceneLoadTime + startOfTheCall&&started[0]==false)
        {
          
            sound.Play();
            started[0] = true;
           
        }
        if (Time.time > sceneLoadTime + startOfTheCall+2 && started[1] == false)
        {
            guiElement.SetActive(true);
            started[1] = true;
        }
        if (guiElement.activeInHierarchy == true && Input.GetButtonDown("Use"))
        {
            print("phonecall has been started");
            guiElement.SetActive(false);
            sound.Stop();
            dialogueObj.GetComponent<linearDialog>().active = true;
            print("dialogue has been started");
            dialogueObj.GetComponent<RunawayScript>().done = true;
        }
    }
}
