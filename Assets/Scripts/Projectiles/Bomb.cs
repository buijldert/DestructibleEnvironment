using UnityEngine;
using System.Collections;


public class Bomb : MonoBehaviour {

    //explosionradius
    [SerializeField]private CircleCollider2D _destructionCircle;
    //explosion animation object
    [SerializeField]private GameObject _explosion;

    //send event with collider to whomever will listen
    public delegate void DestroyGroundAction(CircleCollider2D cc);
    public static event DestroyGroundAction OnDestroyGround;

    //check collision with ground
	private void OnCollisionEnter2D( Collision2D coll )
    {

		if( coll.collider.tag == "Ground" )
        {
            if(OnDestroyGround != null)
            {
                OnDestroyGround(_destructionCircle);
                Destroy(gameObject);
            }
		}
	}

    //spawn explosion when bomb is destroyed
    private void OnDisable()
    {
        Instantiate(_explosion, transform.position, transform.rotation);
    }
}
