using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour {

    public bool LockOnStart = true;
    private bool isLocked = false;

	// Use this for initialization
	void Start () {
        if (LockOnStart)
            SetCursorLock(true);
        else
            SetCursorLock(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && isLocked)
            SetCursorLock(false);
        else if (Input.anyKeyDown && !isLocked && !Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Tab))
            SetCursorLock(true);
	}

    public void SetCursorLock(bool shouldLock)
    {
        if(shouldLock)
        {
            //Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isLocked = false;
        }
    }
}
