using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_FollowCharacter : MonoBehaviour {

    private Transform followTarget;

	// Use this for initialization
	void Start () {
        followTarget = transform.parent;

        transform.SetParent(null); //Become batman.
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Slerp(transform.position, followTarget.position, 30 * Time.smoothDeltaTime);
	}
}
