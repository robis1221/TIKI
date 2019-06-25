using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDialogue : MonoBehaviour {

    public GameObject guiObject;
    public GameObject dialogueObj;
    private bool started = false;
    // Use this for initialization
    void Start()
    {
        guiObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player"&&started==false)
        {
            guiObject.SetActive(true);
            if (guiObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
            {
                started = true;
                guiObject.SetActive(false);
                dialogueObj.GetComponent<linearDialog>().active=true;
                print("dialogue has been started");
            }
        }
    }

    void OnTriggerExit()
    {
        guiObject.SetActive(false);
    }
}
