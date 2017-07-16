using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCharSheet : MonoBehaviour {

    public GameObject charSheet;
    private CursorLock lookScript;

    private bool showInventory = false;

    private void Start()
    {
        lookScript = Camera.main.transform.GetComponent<CursorLock>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
	}

    private void ToggleInventory()
    {
        showInventory = !showInventory;
        charSheet.SetActive(showInventory);

        if (showInventory == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
