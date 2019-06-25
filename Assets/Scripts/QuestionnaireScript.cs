using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;


public class QuestionnaireScript : MonoBehaviour {
    public Text storyOfTheGame;
    public Text continuation;
    public Toggle[] likertScale;
    public Button submit;
    public bool sceneChange;
    private static int currentLvl;
    public RawImage img;
    public Text txtToDisable;
    public GameObject inpToDisable;
    public Text txtToChange;


    void Start()
    {
        UnFade();//unfade the screen
        submit.onClick.AddListener(changeLevel);//listener for changing the level
        currentLvl = GameObject.FindWithTag("CurrentLevel").GetComponent<CurrentScene>().currentLevel;//find the current level objects level variable so we can find if this was loaded after the street scene or the jail scene
        Cursor.lockState = CursorLockMode.None;//unlock the cursor
        Cursor.visible = true;
        if (currentLvl == -1)
        {
            txtToDisable.text="";
            inpToDisable.SetActive(false);
            txtToChange.text = "How much do you want to start?";
        }
        else if (currentLvl == 0)
        {
            txtToDisable.text = "In your opinion, what happened before day 1?";
        }
        else if (currentLvl == 1)
        {
            txtToDisable.text = "In your opinion, what happened between day 1 and day 2?";
        }
        else if (currentLvl == 2)
        {
            txtToDisable.text = "In your opinion, what happened between day 2 and day 3?";
        }
    } 
    void changeLevel()//gets called once the submit button gets pressed
    {
        int selected = 0;//used to see which toggle was selected
        //print(storyOfTheGame.text);
        for (int i = 0; i < likertScale.Length; i++)//go through all of the toggles to see which one is enabled
        {
            if (likertScale[i].isOn)
            {
                selected = i + 1;
                print(i + 1 + " is on");
            }
        }
        //print(continuation.text);
        WriteString(storyOfTheGame.text, selected, continuation.text);//write the story, the toggle nr, and the continuation to a text file
        //print("level done");
        StartCoroutine(LoadLevel());//call the load next level
    }
    //[MenuItem("Tools/Write file")]
    static void WriteString(string story, int enabled, string why)//write everything to text
    {

        StreamWriter writer = GameObject.FindWithTag("CurrentLevel").GetComponent<CurrentScene>().outputFile;
        if (currentLvl == -1)
        {
            writer.WriteLine(Time.time + "," + "Submitted Q1: " + story + " Q2: " + enabled + " Q3: " + why);
        }
        else if (currentLvl == 0)//differentiations. if a line ends with *, it was the first instance of questionnaire, _ is the second instance
                                 //just to make it easier to parse the text file later
        {
            writer.WriteLine(Time.time + "," + "Submitted Q1: " + story + " Q2: " + enabled + " Q3: " + why);
        }
        else if (currentLvl == 1)
        {
            writer.WriteLine(Time.time + "," + "Submitted Q1: " + story + " Q2: " + enabled + " Q3: " + why);
        }
        else if (currentLvl == 2)
        {
            writer.WriteLine(Time.time + "," + "Submitted Q1: " + story + " Q2: " + enabled + " Q3: " + why);
        }
        // writer.Close();
    }
    public IEnumerator LoadLevel()
    {
     //   Debug.Log("calledloadlevel");
        Fade();//fade the level
        yield return new WaitForSeconds(3);//and wait for 3 secs
        if (currentLvl == -1)
        {
            SceneManager.LoadScene("Environment", LoadSceneMode.Single);//load a level
        }
        else if (currentLvl == 0)//depending on the previous level
        {
            SceneManager.LoadScene("JailScene", LoadSceneMode.Single);//load a level
        }
        else if (currentLvl == 1)
        {
            SceneManager.LoadScene("Scene01Street", LoadSceneMode.Single);
        }
        else if (currentLvl == 2)
        {
            Application.Quit();
        }
    }
    public void Fade()
    {
        img.CrossFadeAlpha(1.0f, 2.0f, false);

    }
    public void UnFade()
    {
        img.CrossFadeAlpha(0.0f, 2.0f, false);
    }
}
