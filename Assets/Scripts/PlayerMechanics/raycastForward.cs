using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastForward : MonoBehaviour {
	// Update is called once per frame

	//public ObjectsToCollect otc;
	public RaycastHit hit;
	public float theDistance;
	public float pickUp = 3;
	//public Camera cam;

	public void Update () {
		//cam = GetComponent<Camera>();
		//Ray ray = cam.ViewportPointToRay(Vector3.forward);
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10; //here we cast how far it's going to go. Units are in meters.
	    Debug.DrawRay(transform.position, forward, Color.green);

							
		if(Physics.Raycast(transform.position, forward, out hit)){
			theDistance = hit.distance; //hit is our Raycast and distance is the distance to whatever we hit 
			//print (theDistance + " " + hit.collider.gameObject.name);  //10m away name of game object that got hit 

			if (Input.GetKeyUp(KeyCode.E) && theDistance < pickUp){
				Collider bc = hit.collider as Collider;
				if (bc != null && bc.gameObject.tag == "cue")
				{
					bc.gameObject.SetActive(false); //the game object you just collided with is going to get set to false
					//bc:otc.CubeDelete(); - something like this maybe, idk ... 
				//	ObjectsToCollect.objects--;
                 //   ObjectsToCollect.collectedObjects++;
				//	print(ObjectsToCollect.objects);
				}
			}		
		}	
	}
}
