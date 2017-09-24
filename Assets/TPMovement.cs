using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMovement : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = transform.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Horiz", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vert", Input.GetAxis("Vertical"));
        transform.GetChild(0).Rotate(transform.up, Input.GetAxis("Horizontal") * 55 * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (transform.GetChild(0).transform.forward * 3f * Time.deltaTime * Input.GetAxis("Vertical")));

    }
}
