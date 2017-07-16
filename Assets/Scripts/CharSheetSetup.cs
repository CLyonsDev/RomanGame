using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSheetSetup : MonoBehaviour {

    public Text staminaText, healthText, xpText;

    public Movement moveScript;
    public XPSystem xpScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //staminaText.text = (int)moveScript.stamina + "/" + (int)moveScript.maxStamina;
        //xpText.text = xpScript.currentXP.ToString() + "/" + xpScript.xpToNextLevel.ToString();
	}
}
