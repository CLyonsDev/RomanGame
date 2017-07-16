using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobNew : MonoBehaviour {

    private float timer = 0.0f;
    float bobbingSpeed = 0.18f;
    float bobbingAmount = 0.2f;
    float midpoint = 2.0f;

    float waveslice;


    void Update () {
        waveslice = 0.0f;
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Mathf.Abs(horiz) == 0 && Mathf.Abs(vert) == 0)
            timer = 0;
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
                timer = timer - (Mathf.PI * 2);
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horiz) + Mathf.Abs(vert);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
        }
        else
            transform.localPosition = new Vector3(transform.localPosition.x, midpoint, transform.localPosition.z);
	}
}
