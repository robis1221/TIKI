using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
	[SerializeField] private string mouseXInputName, mouseYInputName;
	[SerializeField] private float mouseSensitivity; 

	[SerializeField] private Transform playerBody; //this acceses the playerbody's transform variable

	Transform dialougeTarget;

	private bool isInDialouge = false;

	private float xAxisClamp;

	private float yAxisClamp;

	public bool sitting;

	Scene myScene;

	string sceneName;

	private void Awake(){
		lockCursor(); 
		xAxisClamp = 0.0f;
		yAxisClamp = 0.0f;
		myScene =  SceneManager.GetActiveScene();
		sceneName = myScene.name;
	}

	public void lockCursor(){
		Cursor.lockState = CursorLockMode.Locked;
		dialougeTarget = null;
        Cursor.visible = false;
      	transform.localRotation = Quaternion.Euler(0,0,0);
	}


	public void unlockCursor(){
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
   
   	public void unlockCursorAndLookAt(Transform target){
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		dialougeTarget = target;
	}
	private void Update(){
		if(Cursor.lockState == CursorLockMode.None && dialougeTarget != null){
			transform.LookAt(dialougeTarget);
			if(sceneName == "Scene01Street"){
			transform.rotation = Quaternion.Euler(-11.25f, 0.0f, 0.0f);
			}
		}else{
            cameraRotation();
		}
	}
   
	private void cameraRotation(){
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; 
		xAxisClamp += mouseY;
		yAxisClamp += mouseX;

		if(xAxisClamp > 90.0f){
			xAxisClamp = 90.0f; //clamps the value to 90
			mouseY = 0.0f; //by setting this to 0, we apply a rotation of 0 whenever the rotation is more than 90. so it can't move further. 
			ClampXAxisRotationToValue(270.0f); //add 270 degrees , for some reason (the 90 degree angle was very overshot previously.)
		}

		else if(xAxisClamp < -90.0f){
			xAxisClamp = -90.0f; //clamps the value to -90
			mouseY = 0.0f; //by setting this to 0, we apply a rotation of 0 whenever the rotation is more than 90. so it can't move further. 
			ClampXAxisRotationToValue(90.0f);
		}
		//transform.Rotate(-transform.right * mouseY); //why -transform.right ????

		transform.Rotate(Vector3.left * mouseY); //why Vector3.right ????
		playerBody.Rotate(Vector3.up * mouseX); //why Vector3.up ?? 

		// If wa are "sitting"
		if(sitting){
			// does we look 90 degrees to the right
			if(yAxisClamp > 90.0f){
			//	print("OVER 9000!");
				//playerBody.transform.Rotate(Vector3.up, 90.0f);
			// do we look 90 degrees to the left	
			}else if(yAxisClamp < - 90.0f){
			//	print("UNDER 9000!");
			}
		}
	}

	private void ClampXAxisRotationToValue(float value){
		Vector3 eulerRotation = transform.eulerAngles; //gets the euler coord. agles
		eulerRotation.x = value; //hardcodes the x-axis to the value we passed to the funct.
		transform.eulerAngles = eulerRotation; //sets the euler angles back to the value of eulerRotation
	}

    // Use this for initialization
    void Start () {
		
	}
	
	//methods for controling players in dialouge
	
	// Update is called once per frame
	
}
