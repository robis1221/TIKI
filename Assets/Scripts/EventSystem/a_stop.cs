



    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_stop : MonoBehaviour
{
    public AudioSource a_source;
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
            controller.SimpleMove(-transform.forward * speed);
    }

    

// Update is called once per frame
void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "guard")
        {
            a_source.Stop();
            Debug.Log("audio stopped");
            Destroy(this);
        }
	}
}
