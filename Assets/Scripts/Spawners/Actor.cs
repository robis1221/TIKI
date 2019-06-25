using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// Use this for initialization
	public float actorSpeed = 1; // how fast does this boi move
	public float interactionRange;	// within which range can you interact with the actor

	public int actionToPerform = 0; //which action the actor performs when it reaches the target
	public bool conditionsForActionMet = false;
	public bool waiting = false;

	public bool spawn = false;

	public Transform exitSceneAt;

	bool moving = true;

	public Transform target;

    AudioSource audioData;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if i have done what i need to do
        if (target != null)
        {
            if (!waiting)
            {
                //this.transform.position += new Vector3(0, 1, 0);
                //target.transform.position += new Vector3(0, -0.6f, 0);

                Vector3 direction = target.transform.position - this.transform.position + new Vector3(0, 0, 1f);
                this.GetComponent<Rigidbody>().velocity = direction.normalized * actorSpeed;
                this.transform.localRotation = Quaternion.LookRotation(direction, new Vector3(0, 1, 0));

            }
            else if (waiting)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            }
            //check if within interaction range
            if (Vector3.Distance(target.position, transform.position) < interactionRange)
            {
                //if it is dosomething depending on the actor type
                if (waiting == false)
                {
                    switch (actionToPerform)
                    {
                        case 1:
                            transform.localRotation = Quaternion.Euler(0, 180, 0); // rotate towards player
                            transform.position = transform.position + new Vector3(0, 0, -0.3f); // "Step" slightly towards the player
                            gameObject.GetComponent<linearDialog>().active = true;
                            waiting = true;
                            break;
                    }

                }

                if (actionToPerform == 2 && !conditionsForActionMet)
                { // throw coin conditions

                    GameObject coinObj = Instantiate(Resources.Load<GameObject>("coin"));
                    audioData = GetComponent<AudioSource>();
                    audioData.Play();
                    
                    coinObj.transform.position = transform.position;

                    DestroyAfterXDist distance = coinObj.AddComponent<DestroyAfterXDist>();
                    distance.startPos = transform.position;
                    float dist = Vector3.Distance(target.position, transform.position);
                    distance.distanceAfterWhichToDestory = dist;
                    distance.Buffer = 0.7f;

                    coinObj.AddComponent<Rigidbody>().velocity = (target.position - transform.position).normalized * dist * 2;

                    conditionsForActionMet = true;
                    target = exitSceneAt;

                }

            }
            //check if interaction is done 
            switch (actionToPerform)
            {
                case 1:
                    if (gameObject.GetComponent<linearDialog>() == null && waiting)
                    {
                        conditionsForActionMet = true;
                        waiting = false;
                        target = exitSceneAt;
                    }
                    break;
            }
        }
		
	}
}
