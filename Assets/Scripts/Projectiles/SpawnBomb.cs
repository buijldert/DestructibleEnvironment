using UnityEngine;
using System.Collections;

public class SpawnBomb : MonoBehaviour {

    //set bomb object
    [SerializeField]private GameObject _bomb;
    //set continuous bomb dropping rate
    [SerializeField]private float _bombDroprate = .1f;
    //check if bombs are dropping or not
    private bool _isDroppingBombs;

    //add functions to corresponding events
    void OnEnable()
    {
        MouseInput.OnLeftClickHold += StartDroppingBombs;
        MouseInput.OnLeftClickUp += StopDroppingBombs;
    }

    //start dropping bombs
    private void StartDroppingBombs()
    {
        if(_isDroppingBombs == false)
        {
            StartCoroutine(DropBombs());
            _isDroppingBombs = true;
        }
    }

    //stop dropping bombs
    private void StopDroppingBombs()
    {
        _isDroppingBombs = false;
    }
    
    //drop bombs continuously while left mouse button is held down
    private IEnumerator DropBombs()
    {
        Instantiate(_bomb, DetermineMousePosition(), transform.rotation);
        yield return new WaitForSeconds(_bombDroprate);
        if(_isDroppingBombs)
        {
            StartCoroutine(DropBombs());
        }
    }

    //determine the position of the mouse in screenspace to wordspace
    private Vector3 DetermineMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    //remove functions from corresponding events
    void OnDisable()
    {
        MouseInput.OnLeftClickHold -= StartDroppingBombs;
        MouseInput.OnLeftClickUp -= StopDroppingBombs;
    }
}
