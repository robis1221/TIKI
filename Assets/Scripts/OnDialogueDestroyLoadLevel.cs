using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnDialogueDestroyLoadLevel : MonoBehaviour {
    public GameObject gameEnder;
    public string levelToLoad;
    public RawImage img;

	void Update () {
        if (gameEnder.GetComponent<linearDialog>() == null)
        {
         
                StartCoroutine(LoadLevel(levelToLoad));
    
        }	
	}

    public IEnumerator LoadLevel(string level)
    {
        Debug.Log("calledloadlevel");
        //SceneManager.LoadScene(level, LoadSceneMode.Single);
        //AsyncOperation AO = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        //AO.allowSceneActivation = false;
        Fade();
        yield return new WaitForSeconds(3);
        if (levelToLoad == "Exit")
        {
            print("Game Ended");
            Application.Quit();
        }
        else
        {

            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }
        //Fade the loading screen out here

        //AO.allowSceneActivation = true;
     //   ObjectsToCollect.objects = 0;
      //  ObjectsToCollect.collectedObjects = 0;


    }
    public void Fade()
    {
        img.CrossFadeAlpha(1.0f, 2.0f, false);
        //while (img.alpha < 1)
        //{
        //  img.alpha += Time.deltaTime / 20;

        //}
    }
    public void UnFade()
    {
        img.CrossFadeAlpha(0.0f, 1.0f, true);
        //while (img.alpha >0)
        //{
        //    img.alpha -= Time.deltaTime / 20;

        //  }
    }
}
