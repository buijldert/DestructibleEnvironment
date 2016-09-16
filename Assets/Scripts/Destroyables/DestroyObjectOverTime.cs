using UnityEngine;
using System.Collections;

public class DestroyObjectOverTime : MonoBehaviour {

    //destroy time
    [SerializeField]private float _destroyTime;

    //startcoroutine to destroy in _destroyTime seconds
    private void Start()
    {
        StartCoroutine(DestroyOverTime());
    }

    //destroy object in _destroyTime seconds
    private IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }
}
