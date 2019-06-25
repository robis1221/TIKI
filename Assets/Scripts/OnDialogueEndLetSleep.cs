using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDialogueEndLetSleep : MonoBehaviour
{
    public GameObject dialogueObject;
    public GameObject dTrigger;
    public GameObject lTrigger;
    public GameObject dGui;

    void Update()
    {
        if (dialogueObject.GetComponent<linearDialog>() == null)
        {
           // dGui.SetActive(false);
            dTrigger.SetActive(false);
            lTrigger.SetActive(true);
            enabled = false;//disable the script so it stops updating
        }
    }
}
