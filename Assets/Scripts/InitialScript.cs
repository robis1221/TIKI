using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialScript : MonoBehaviour {
    public Button submit;
    public Text inputfield;
    public int nr;
    private int accessed = 0;
    public GameObject curr;

	// Use this for initialization
	void Start () {
        submit.onClick.AddListener(changeLevel);//listener for changing the level
        Cursor.lockState = CursorLockMode.None;//unlock the cursor
        Cursor.visible = true;
        DontDestroyOnLoad(this);
    }
    public int getNR()
    {
        accessed++;
        return nr;
    }
    void Update()
    {
        if (accessed >= 2)
        {
            accessed++;
        }
        if (accessed > 5)
        {
            Destroy(this);
        }
    }

	
	// Update is called once per frame
	void changeLevel () {
        nr =int.Parse(inputfield.text);
        curr.GetComponent<CurrentScene>().callTheStart();
        curr.GetComponent<PlayerEmotions>().callTheStart();
        print(nr);

        SceneManager.LoadScene("Questionnaire");
        
    }
}
