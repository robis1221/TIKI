using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunawayScript : MonoBehaviour {
    public float speed = 6.0f;
    public Transform target;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public bool done = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) < 7)
        {
            transform.LookAt(target);
            transform.Rotate(0, 270, 0);
            controller.SimpleMove(-transform.right*speed);
        }
        else if (done&& Vector3.Distance(target.position, transform.position) < 20)
        {
            transform.LookAt(target);
            transform.Rotate(0, 270, 0);
            controller.SimpleMove(-transform.right * speed*2);
        }
        
    }
}
