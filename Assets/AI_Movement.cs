using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator anim;
    public Transform target;

    Vector3 prevPos;
    public float curSpd;

	// Use this for initialization
	void Start () {
        agent = GetComponentInChildren<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        Vector3 curMove = transform.position - prevPos;
        curSpd = curMove.magnitude / Time.deltaTime;
        prevPos = transform.position;

        anim.SetFloat("Vert", Mathf.Lerp(anim.GetFloat("Vert"), (curSpd / anim.speed), 10 * Time.deltaTime));
	}
}
