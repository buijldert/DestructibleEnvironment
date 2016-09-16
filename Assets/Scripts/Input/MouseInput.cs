using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

    //set event for leftclick hold
    public delegate void LeftClickHoldAction();
    public static event LeftClickHoldAction OnLeftClickHold;

    //set event for left click up
    public delegate void LeftClickUpAction();
    public static event LeftClickUpAction OnLeftClickUp;

    // Update is called once per frame
    void Update ()
    {
        MouseButtonsInput();
	}

    //check if mouse input is used
    private void MouseButtonsInput()
    {
        if(Input.GetMouseButton(0) && OnLeftClickHold != null)
        {
            OnLeftClickHold();
        }

        else if(Input.GetMouseButtonUp(0) && OnLeftClickUp != null)
        {
            OnLeftClickUp();
        }
    }
}
