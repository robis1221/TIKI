using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour {
    private float x=0;
    private float rotationSpeed=-450;
	void Update () {
        x = Time.time * rotationSpeed;
        transform.localRotation = Quaternion.Euler(x, 0, 0);
	}
}
