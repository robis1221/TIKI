using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;
//this script is used just to pass the previous scene to the questionnaire scene, since were using only one scene for the questionnaire it needs to know if its the first or second instance
public class CurrentScene : MonoBehaviour {
    public int currentLevel;
    private bool doneWithSettingLevel = true;
    public StreamWriter outputFile;
    public GameObject talker;
    private int talkerS1Increment=0;
    private linearDialog dialog;
    private int oldDialog=0;
    private int getDialog;
    private bool started=false;
    private bool threwTheCoin = false;
    public GameObject giver;
    private GameObject participant;
    private bool calledTheStart = false;

   public void callTheStart () {
        print("calledstartcurr");
        participant = GameObject.FindWithTag("ParticipantNumberObject");
        if (participant.GetComponent<InitialScript>() != null)
        {
            outputFile = new StreamWriter(participant.GetComponent<InitialScript>().getNR()+"_VAGUE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "PlayerTimings.csv");
        }
        else
        {
            outputFile = new StreamWriter("CONCRETE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "PlayerTimings.csv");
        }
        
        outputFile.WriteLine("Time,Event");
        SetLevel();
        calledTheStart = true;
    }
    void Update()
    {
        if (calledTheStart)
        {
            if (SceneManager.GetActiveScene().name == "Scene01Street" && doneWithSettingLevel == true)
            {
                SetLevel();
            }
            if (SceneManager.GetActiveScene().name == "JailScene" && doneWithSettingLevel == false)
            {
                SetLevel();
            }
            if (SceneManager.GetActiveScene().name == "Environment" && doneWithSettingLevel == true)
            {
                SetLevel();
            }
            if (SceneManager.GetActiveScene().name == "Scene01Street")
            {

                giver = GameObject.FindWithTag("giver");

                if (giver != null && threwTheCoin == false && giver.GetComponent<Actor>().conditionsForActionMet)
                {
                    // print("spawnenter");
                    writeToFile("received a coin");
                    threwTheCoin = true;
                }
                if (giver == null && threwTheCoin)
                {
                    threwTheCoin = false;
                }
            }
            if (started == false)
            {
                talker = GameObject.FindWithTag("talker");
            }
            if (talker != null)
            {
                dialog = talker.GetComponent<linearDialog>();
                if (dialog != null)
                {
                    getDialog = talker.GetComponent<linearDialog>().currentDialog;
                }
                if (dialog != null && dialog.active && started == false)
                {
                    writeToFile("dialog nr. " + talkerS1Increment + " started");
                    started = true;

                }
                if (started && dialog != null && oldDialog != getDialog)
                {
                    if (dialog.pressedButton == -1)
                    {
                        writeToFile("Line  " + (oldDialog + 1) + " appeared with talker " + (talkerS1Increment + 1));
                    }
                    else
                    {
                        writeToFile("Line  " + (oldDialog + 1) + " appeared with talker " + (talkerS1Increment + 1) + " and player made the choice " + (dialog.pressedButton + 1));
                    }
                    oldDialog = getDialog;
                }
                if (started && dialog == null)
                {
                    oldDialog = 0;
                    talkerS1Increment += 1;
                    started = false;
                }
            }
        }
    }
    void SetLevel()
    {
        if (SceneManager.GetActiveScene().name == "StartingScene")
        {
            doneWithSettingLevel = true;
            currentLevel = -1;
        }
        else if (SceneManager.GetActiveScene().name == "Scene01Street")
        {
            currentLevel = 2;
            doneWithSettingLevel = false;
        }
        else if (SceneManager.GetActiveScene().name == "JailScene")
        {
            currentLevel = 1;
            doneWithSettingLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Environment")
        {
            currentLevel = 0;
            doneWithSettingLevel = false;
        }
        writeToFile("Scene " + (currentLevel + 1) + " loaded");
        // print(currentLevel);
        DontDestroyOnLoad(this);
    }
    void writeToFile(string ev)
    {
        outputFile.WriteLine(Time.time + "," + ev);
    }
    void OnApplicationQuit()
    {
        outputFile.Flush();
        outputFile.Close();
    }
}
