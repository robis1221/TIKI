using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BegScript : MonoBehaviour
{
    public GameObject Sound;
    public GameObject guiObject;
    public GameObject cup;
    private bool active=false;
    private int startTime;
    private GameObject talker;
    Quaternion rot;
    private bool hidden=false;
    private bool doneWithShowing = true;
    // Use this for initialization
    void Start()
    {
        guiObject.SetActive(true);
       // rot = cup.transform.rotation;
    }

    void Update()
    {
        talker = GameObject.FindWithTag("talker");
        if (talker != null&& talker.GetComponent<linearDialog>() != null&& talker.GetComponent<linearDialog>().active)
        {
            hidden = true;
            doneWithShowing = false;
        }
        else{ hidden = false; }
        if (hidden)
        {
            cup.SetActive(false);
            guiObject.SetActive(false);
        }
        else {
            if (doneWithShowing == false)
            {
                cup.SetActive(true);
                guiObject.SetActive(true);
                doneWithShowing = true;
            }
            if (guiObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
            {
                guiObject.SetActive(false);
                active = true;
                startTime = Time.frameCount;
                Sound.GetComponent<soundTrigger>().s_Trigger = true;

            }
            if (active == true)
            {
           //     print("running " + Time.frameCount);
                if (Time.frameCount < startTime + 20)
                {
                    print("Right " + Time.frameCount);
                    cup.transform.Rotate(Vector3.right*0.3f);

                }
                if (Time.frameCount >= startTime + 20 && Time.frameCount < startTime + 60)
                {
                    print("Left " + Time.frameCount);
                    cup.transform.Rotate(Vector3.left*0.3f);
                }
                if (Time.frameCount >= startTime + 60 && Time.frameCount < startTime + 80)
                {
                    print("Right " + Time.frameCount);
                    cup.transform.Rotate(Vector3.right*0.3f);
                }
                if (Time.frameCount > startTime + 80)
                {
                    print("Done " + Time.frameCount);
                    // cup.transform.rotation = rot;
                    guiObject.SetActive(true);
                    active = false;

                }
            }
        }
    }
}
