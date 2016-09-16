using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadScene : MonoBehaviour {

    private void OnEnable()
    {
        KeyboardInput.OnSpacebarDown += ReloadTheScene;
    }

	private void ReloadTheScene()
    {
        SceneManager.LoadScene("main");
    }

    private void OnDisable()
    {
        KeyboardInput.OnSpacebarDown -= ReloadTheScene;
    }
}
