using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class dialogInstance : System.Object{
	public string npcSays; //what does the npc say
	public List<string> playerAnswers;

	public AudioClip npcSound;

	public float showLength; // 0 if linearDialog gets to decide, otherwise you set length in seconds here

	
}
public class linearDialog : MonoBehaviour {

	public List<dialogInstance> fullDialog; // contains all the dialog in the conversatoin
	public GameObject player;
	private AudioSource audioSource;

	private GameObject UI;
	
	public GameObject ButtonPrefab; 

	public int FontSize = 20;
	public Vector2Int textHeightAndWith = new Vector2Int(1200,350);

	public bool active = false;
	bool showingDialog = false;

	bool counting = false;
	public int currentDialog = 0;

	float timeToChangeText = 0;


	GameObject textField;

	public Animation animation;
    public int pressedButton = -1;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>(); // set the component
		animation = GetComponent<Animation>();
		
	}
	
	// Update is called once per frame
	void Update () {
//		print(currentDialog + " " + timeToChangeText + " : " + Time.time+  " am i counting ? :" + counting);
		//if no dialogue do NOTHING
		if(fullDialog.Count < 1 || ButtonPrefab == null){return;}

		if(active && !showingDialog){
			//this means we should setup to show dialog
			
			//check if there is a UI tagged object in scene if not do nothing
			if(GameObject.FindGameObjectsWithTag("UI").Length < 1){ return;}

			//disable player movement and make UI clickable
			PlayerMove playerMover = player.GetComponent<PlayerMove>();
		//	if(playerMover != null){
				playerMover.goIntoDialouge(this.gameObject.transform);
			//}
        
			UI = GameObject.FindGameObjectsWithTag("UI")[0];


			GameObject textField = new GameObject("npcText", typeof(RectTransform));
     		textField.AddComponent<Text>();
			textField.transform.parent = UI.transform;
			textField.transform.localScale = new Vector3 (1,1,1);
			textField.transform.position = UI.transform.position;
            textField.transform.localPosition += new Vector3(0, -450, 0);
            textField.transform.rotation = UI.transform.rotation;
            textField.AddComponent<Outline>();
            textField.GetComponent<Outline>().effectDistance = new Vector2(2, 2);
            //frame.rotation.SetEulerAngles(0,0,0);

            GameObject buttonParent = new GameObject("ButtonParrent");
			buttonParent.transform.parent = UI.transform;
			buttonParent.transform.localScale = new Vector3(1,1,1);
			buttonParent.transform.position = UI.transform.position;
            buttonParent.transform.localPosition += new Vector3(0, -200, 0);
            buttonParent.transform.rotation = UI.transform.rotation;

			Text npcSays = textField.GetComponent<Text>();
			
			npcSays.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			npcSays.fontSize = FontSize;
			npcSays.rectTransform.sizeDelta = textHeightAndWith;
			

			showingDialog = true;
			startDialog(0);
            audioSource.Play();//play audio
		}

		if(counting){
			if(Time.time > timeToChangeText && counting){
				
				counting = false;
				showAnswers();
			}
		}

		
	}

	void startDialog(int dialogIndex){
        // make sure no buttons are showing
     
            GameObject man = GameObject.FindGameObjectWithTag("animation");
        if (man != null)
        {
            man.GetComponent<Animation>().Stop("walk 1");
        }
            
       
		GameObject buttonParent = GameObject.Find("ButtonParrent");
		foreach(Transform child in buttonParent.transform){
			Destroy(child.gameObject);
		}

		Text npcSays = GameObject.Find("npcText").GetComponent<Text>(); 
		npcSays.text = fullDialog[dialogIndex].npcSays;
        audioSource.clip = fullDialog[dialogIndex].npcSound; //adding npc sound to audio source
		
		if(fullDialog[dialogIndex].showLength > 0){
			timeToChangeText = Time.time + fullDialog[dialogIndex].showLength;
		}else if(fullDialog[dialogIndex].npcSound != null){
			timeToChangeText =  Time.time + fullDialog[dialogIndex].npcSound.length;
		}else{
			timeToChangeText = Time.time + fullDialog[dialogIndex].npcSays.Length * 0.05f;
		}
		counting = true;
	}

	void showAnswers(){
		
		if(fullDialog[currentDialog].playerAnswers.Count < 1){
			OnClickGoToNextDialouge(-1);
		}else
		{
		//create button to answer dialouge
		dialogInstance dialog = fullDialog[currentDialog];

		//make them children of the button parent object.
		GameObject buttonParent = GameObject.Find("ButtonParrent");

		//create a button for each answer 
		for(int i = 0 ; i < dialog.playerAnswers.Count; i++){
				GameObject button = createButton(buttonParent.transform, 0,i*120 + 20,i);
				Text buttonText = button.transform.GetComponentInChildren<Text>();
				buttonText.text = dialog.playerAnswers[i];
			}
		}
	}

	GameObject createButton(Transform Parent, int xPot, int yPos, int buttonNr){
		GameObject button = Instantiate(ButtonPrefab,Parent);
		button.transform.localPosition = new Vector3(xPot,-yPos,0);
		Button buttonComponment = button.GetComponent<Button>();
        buttonComponment.onClick.AddListener(delegate { OnClickGoToNextDialouge(buttonNr); });
		return button;
	}

	void cleanUpThenSuicide(){
		PlayerMove playerBody = player.GetComponent<PlayerMove>();
		playerBody.goOutOfDialouge();
		DestroyImmediate(GameObject.Find("ButtonParrent"));
		Destroy(GameObject.Find("npcText"));
		GameObject man = GameObject.FindGameObjectWithTag("animation");
        if (man != null)
        {
            man.GetComponent<Animation>().Play("walk 1");
        }
		Destroy(this);

	}

	void OnClickGoToNextDialouge(int no) // used to tell unity what happens when we press a button
    {
        currentDialog++;
        pressedButton = no;
		if(currentDialog < fullDialog.Count){
			startDialog(currentDialog);
            audioSource.Play(); //play the dialogue
		}else{
			cleanUpThenSuicide();
		}
		
			
    }
	
}
