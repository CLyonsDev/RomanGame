using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

    public float destroyDelay = 3f;

	void Start () {
        StartCoroutine(DelayedDestroy());
	}

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(this.gameObject);
    }
}
