using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public Transform lookTarget;
    private float lookSpeed = 1f;

    void Start()
    {
        transform.LookAt(lookTarget.position);
    }

	void FixedUpdate () {
        //transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.LookRotation(lookTarget.transform.position, transform.position)), Time.fixedDeltaTime * lookSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget.position - transform.position), lookSpeed * Time.fixedDeltaTime);
	}
}
