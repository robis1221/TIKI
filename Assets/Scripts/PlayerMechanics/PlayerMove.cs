using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField] private string horizontalInputName;
	[SerializeField] private string verticalInputName;
	[SerializeField] private float movementSpeed;
	private CharacterController charController;

	//////////////player jump fields////////////
	[SerializeField] private AnimationCurve jumpFallOff; //of type AnimationCurve 
	[SerializeField] private float jumpMultiplier; //multiplies the value of the AnimationCurve
	[SerializeField] private KeyCode jumpKey;
	private bool isJumping;

	bool inDialouge = false;
	bool canMove = true;

	Transform dialougeTarget;
	

	private void Awake(){
		charController = GetComponent<CharacterController>(); //gets the character controller from inptu I think, not exactly sure 
	}

	private void Update(){
		if(canMove){
			PlayerMovement();
		}else{
			lookAtDialougeTarget();
		}
		
	}

	///////////// THE MOVEMENT PART //////////////////////

	private void PlayerMovement(){
		float vertInput = Input.GetAxis(verticalInputName);
		float horizInput = Input.GetAxis(horizontalInputName);

		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

		charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed); 
		//Vector3.ClampMagnitude makes sure the speed of movement does not exceed 1, which is the max speed on a single axis. This makes all the speed consistent. 
		
		JumpInput();
	}
	
	///////////// THE JUMP PART //////////////////////

	private void JumpInput(){ // this part gets the conditions for the JumpEvent to be triggered
		if(Input.GetKeyDown(jumpKey) && !isJumping){
			isJumping = true;
			StartCoroutine(JumpEvent());
		}
	}
	private IEnumerator JumpEvent(){ //type IEnumetarot because it's used as a co-routine (dafuq is that ? )
		charController.slopeLimit = 90.0f; //by adjunsting the to 90, it can jump on obstacles without the strange jitter.
		
		float timeInAir = 0.0f;
		do{
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime); //makes character go up.
			timeInAir += Time.deltaTime; // adds a second to timeInAir

			yield return null; //so we don't get any by-products 
		} while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);  

		charController.slopeLimit = 45.0f;
		
		//grounded is another condition, maybe he is tied to something, idk ???
		//if the player hits a ceiling, the motion stops and he falls back down


		/////now jump event has finished

		isJumping = false; 
	
		
		
		
		

		
	}

	public void goIntoDialouge(Transform _dialougeTarget){
		dialougeTarget = _dialougeTarget;
		unlockCursor();
		inDialouge = true;
		canMove = false;
	}

	public void goOutOfDialouge(){
		lockCursor();
		inDialouge = false;
		canMove = true;
        
		//transform.rotation = Quaternion.EulerAngles(0,transform.rotation.y,0);
	}

	void lookAtDialougeTarget(){
		Vector3 direction = dialougeTarget.position - transform.position;
		direction.x = 0;
		transform.rotation.SetEulerAngles(direction);
	}

	public void lockCursor(){
		GetComponentInChildren<PlayerLook>().lockCursor();
	}

	public void unlockCursor(){
		GetComponentInChildren<PlayerLook>().unlockCursorAndLookAt(dialougeTarget);
	}



}