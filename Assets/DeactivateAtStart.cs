using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAtStart : MonoBehaviour {

	// What else did you expect it to do?
	void Start () {
        this.gameObject.SetActive(false);
	}
}
