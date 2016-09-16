using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {

    //spacebar input event
    public delegate void SpacebarAction();
    public static event SpacebarAction OnSpacebarDown;
	
	// Update is called once per frame
	private void Update ()
    {
        KeyboardButtonInput();
    }

    //keyboard input checker
    private void KeyboardButtonInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && OnSpacebarDown != null)
        {
            OnSpacebarDown();
        }
    }
}
