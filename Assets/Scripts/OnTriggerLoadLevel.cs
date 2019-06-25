using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnTriggerLoadLevel : MonoBehaviour {

	public GameObject guiObject;
	public string levelToLoad;
    public RawImage img;
    public RawImage dayImg;
    private bool doneFade=false;
	// Use this for initialization
	void Start () {
        UnFade();
        if (guiObject != null){
            guiObject.SetActive(false);
        }
	}
	
	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			guiObject.SetActive(true);
			if(guiObject.activeInHierarchy == true && Input.GetButtonDown("Use"))
			{
                print("calling load level");
                StartCoroutine(LoadLevel(levelToLoad));
                //LoadLevel(levelToLoad);
            }
		}
	}

	void OnTriggerExit()
	{
		guiObject.SetActive(false);
	}
   public IEnumerator LoadLevel(string level)
    {
        Debug.Log("calledloadlevel");
        //SceneManager.LoadScene(level, LoadSceneMode.Single);
        //AsyncOperation AO = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        //AO.allowSceneActivation = false;
        Fade();
            yield return new WaitForSeconds(3);

        SceneManager.LoadScene(level, LoadSceneMode.Single);

        //Fade the loading screen out here

        //AO.allowSceneActivation = true;
    //    ObjectsToCollect.objects = 0;
        //ObjectsToCollect.collectedObjects = 0;


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
        dayImg.CrossFadeAlpha(0.0f, 2.0f, false);
        img.CrossFadeAlpha(0.0f, 2.0f, false);
        //while (img.alpha >0)
        //{
        //    img.alpha -= Time.deltaTime / 20;

        //  }
    }
}
