using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupWave : MonoBehaviour {

    float riseSpeed = 0.55f;
    float waveSpeed = 50f;

    float seed;

    private void Start()
    {
        seed = Random.Range(-0.25f, 0.25f);
    }

    void Update () {
        Vector3 upAmt = Vector3.up * riseSpeed * Time.deltaTime;
        Vector3 horizAmt = transform.right * (Mathf.Sin(Time.time * (3 + seed)) * 0.001f);
        Vector3 finalDir = upAmt + horizAmt;

        transform.position += finalDir;
	}
}
