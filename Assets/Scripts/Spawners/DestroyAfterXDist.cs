using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXDist : MonoBehaviour {

	// Use this for initialization
	public Vector3 startPos;
	public float Buffer = 1;
	public float distanceAfterWhichToDestory = 2;
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(startPos,transform.position)> distanceAfterWhichToDestory-Buffer){
			Destroy(this.gameObject);
			
		}
	}
}
