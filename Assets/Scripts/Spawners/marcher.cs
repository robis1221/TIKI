using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class marcher : MonoBehaviour {

	// Use this for initialization
	private bool active = false; // nothing happens if this is not active it used to check that we have all the required things
	private float TimeOfLastRespawn = 0;
	//flow variables
	public int nrOfMarchers; // nr of objects marching at the same time
	public float minSpeed = 1f;// minimum posible speed for marcher
	public float maxSpeed = 2f;//max posible speed
	public int nrOfLanes = 3; // how many lanes are there
    public float roadWidth;

	public float laneWidthInMeters = 1;

	public string test;

	public float spawnDelayInSecs = 0;
	public GameObject endPoint; // marchers walk towards this object, then die
	public GameObject interactionTarget;

	public List<GameObject> marchers = new List<GameObject>(); // the potential objects that can be spawned as marchers
	public List<GameObject> specialActors = new List<GameObject>();
	private int currentActor = 0;
	public List<int> reverseLanes = new List<int>(); //
    public RawImage img;

	void Start () {
		//check we have a goal, and a list of marchers;
		if(marchers.Count > 0 && endPoint.transform.position != null){
			active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if i dont have the requirement of marchers and end point  do nothing
		if(!active){ return;}
	//count nr of children alive
	int liveChildren = this.transform.childCount;
	// if less than nrOfMarcher 
	//spawn until nr is equal to marchers

		if(liveChildren < nrOfMarchers && (Time.time - TimeOfLastRespawn)>spawnDelayInSecs){
			TimeOfLastRespawn = Time.time;

			//find vector towards destroyer
			var marchDirection =  endPoint.transform.position - transform.position;
			float distanceBetweenStartAndEnd = marchDirection.magnitude;
			marchDirection.Normalize();
			//flip the vector 90 degrees by switcing the axis
			var perpendicularToMarchDirection = new Vector3(marchDirection.z,marchDirection.y,-marchDirection.x);

			
			
			// create the object that we are gonna use as a marcher
			GameObject newMarcher;

            //added this section to spawn the special everytime there is no special
            int laneToMarchOn = 0;
            if (laneToMarchOn == 0 && specialActors.Count != 0)
            {
                // check if there's no talker or giver
                bool actorExist = false;
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<Actor>() != null)
                    {
                        actorExist = true;
                    }
                }
                if (actorExist)
                {
                    //randomly generate which lane this is walking on 
                    laneToMarchOn = Random.Range(0, nrOfLanes);
                }
            }

                    //if spawned on lane 0 it's a special case where they might have to interact with the player
                    if (laneToMarchOn == 0 && specialActors.Count != 0){
				// check if there's no talker or giver
				bool actorExist = false;
				foreach(Transform child in transform){
					if(child.GetComponent<Actor>() != null ){
						actorExist = true;
					}
				}
				// if there is not, its time to add a special actor to the scene
				if(!actorExist && currentActor < specialActors.Count){
					newMarcher = Instantiate(specialActors[currentActor],this.transform);
					currentActor++;
					Actor ourActor = newMarcher.GetComponent<Actor>();
					ourActor.target = interactionTarget.transform;
					ourActor.exitSceneAt = endPoint.transform;

					linearDialog talkyBoi = newMarcher.GetComponent<linearDialog>();
                    OnDialogueDestroyLoadLevel lastboye = newMarcher.GetComponent<OnDialogueDestroyLoadLevel>();
					if(talkyBoi != null){
						talkyBoi.player = interactionTarget;
					}
                    if (lastboye != null)
                    {
                        lastboye.img = img;
                    }
					
				}
				else
				{
					//spawn a child as a copy of a random object in the marchers list with me as parrent
					newMarcher = Instantiate(marchers[Random.Range(0,marchers.Count)],this.transform);
				}

			}else{
				//spawn a child as a copy of a random object in the marchers list with me as parrent
				newMarcher = Instantiate(marchers[Random.Range(0,marchers.Count)],this.transform);
			}

			
			
		
			//check if this lane is specified as a lane of reverse flow
			bool reverse = reverseLanes.Contains(laneToMarchOn);
			if(reverse){ // if it is reverse
                         //place the child at endpoint 
                if (laneToMarchOn < nrOfLanes / 2)
                {
                    newMarcher.transform.position = endPoint.transform.position + laneToMarchOn * perpendicularToMarchDirection * laneWidthInMeters;
                }
                else
                {
                    newMarcher.transform.position = endPoint.transform.position + laneToMarchOn * perpendicularToMarchDirection * laneWidthInMeters;
                    newMarcher.transform.position += new Vector3(0,0,roadWidth);
                }
		
				//set it to march towards the startpoint
				newMarcher.GetComponent<Rigidbody>().velocity = -marchDirection*Random.Range(minSpeed,maxSpeed);
				
			}
			else //if it is not reverse
			{
                if (laneToMarchOn < nrOfLanes / 2)
                {
                    //place the child at startpoint 
                    newMarcher.transform.position = transform.position + laneToMarchOn * perpendicularToMarchDirection * laneWidthInMeters;
                }
                else
                {
                    newMarcher.transform.position = transform.position + laneToMarchOn * perpendicularToMarchDirection * laneWidthInMeters;
                    newMarcher.transform.position += new Vector3(0, 0, roadWidth);
                }
                    //set it to march towards the endpoint
                    newMarcher.GetComponent<Rigidbody>().velocity = marchDirection*Random.Range(minSpeed,maxSpeed);
			}
			newMarcher.transform.rotation = Quaternion.LookRotation(newMarcher.GetComponent<Rigidbody>().velocity,new Vector3(0,1,0));
			//make sure the object destroys itself at the endpoint or end point
			newMarcher.AddComponent<DestroyAfterXDist>();
			newMarcher.GetComponent<DestroyAfterXDist>().distanceAfterWhichToDestory = distanceBetweenStartAndEnd;


		}
	
		
	}


	

}