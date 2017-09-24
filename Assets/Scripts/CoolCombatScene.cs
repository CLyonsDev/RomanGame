using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCombatScene : MonoBehaviour {

    public Transform attacker;
    public Transform dyer;
    public ParticleSystem bloodPS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(DoCoolCutscene());
        }
	}

    private IEnumerator DoCoolCutscene()
    {
        attacker.GetComponentInChildren<Animator>().SetTrigger("RunAttk");
        yield return new WaitForSeconds(1.6f);
        bloodPS.Play();
        dyer.GetComponentInChildren<Animator>().SetTrigger("Die");
    }
}
