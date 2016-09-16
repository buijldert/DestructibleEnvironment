using UnityEngine;
using System.Collections;

public class DestroyableTexture : MonoBehaviour {

    //set texture to be destroyed
    [SerializeField]private Texture2D _destroyableTexture2D;

	private SpriteRenderer _spriteRenderer;

	private float _widthWorld, _heightWorld;

	private int _widthPixel, _heightPixel;
    private int _colliderPoints;

	private Color _transparent;

    //set event for sending position
    public delegate void SendPositionAction(Vector2 pos);
    public static event SendPositionAction OnSendPosition;

    //set clone of given texture to be altered
    private void OnEnable()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        if (OnSendPosition != null)
        {
            OnSendPosition(transform.position);
        }

        Bomb.OnDestroyGround += DestroyGround;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        Texture2D texture_clone = Instantiate(_destroyableTexture2D);
        _spriteRenderer.sprite = Sprite.Create(texture_clone, new Rect(0f, 0f, texture_clone.width, texture_clone.height), new Vector2(0.5f, 0.5f), 100f);

        gameObject.AddComponent<PolygonCollider2D>();
        _transparent = new Color(0f, 0f, 0f, 0f);
        InitSpriteDimensions();
    }

    //calculate sprite dimensions
    private void InitSpriteDimensions()
    {
		_widthWorld = _spriteRenderer.bounds.size.x;
		_heightWorld = _spriteRenderer.bounds.size.y;
		_widthPixel = _spriteRenderer.sprite.texture.width;
		_heightPixel = _spriteRenderer.sprite.texture.height;
	}

    //destroy ground
	private void DestroyGround( CircleCollider2D cc )
    {

		V2int c = World2Pixel(cc.bounds.center.x, cc.bounds.center.y);

		int r = Mathf.RoundToInt(cc.bounds.size.x * _widthPixel/_widthWorld);


		int x, y, px, nx, py, ny, d;
		
        //go through every pixel that was hit by the collider and alter them
		for (x = 0; x <= r; x++)
		{
			d = (int)Mathf.RoundToInt(Mathf.Sqrt(r * r - x * x));
			
			for (y = 0; y <= d; y++)
			{
				px = c.x + x;
				nx = c.x - x;
				py = c.y + y;
				ny = c.y - y;

                //set every pixel that was hit transparent
                _spriteRenderer.sprite.texture.SetPixel(px, py, _transparent);
                _spriteRenderer.sprite.texture.SetPixel(nx, py, _transparent);
                _spriteRenderer.sprite.texture.SetPixel(px, ny, _transparent);
                _spriteRenderer.sprite.texture.SetPixel(nx, ny, _transparent);
			}
		}

        //apply altered texture
        _spriteRenderer.sprite.texture.Apply();
        
        //adjust polygon collider
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    //translate world position to pixel position
	private V2int World2Pixel(float x, float y)
    {
		V2int v = new V2int();
		
        //calculate deltaX
		float dx = x-transform.position.x;
		v.x = Mathf.RoundToInt(0.5f * _widthPixel + dx * _widthPixel/_widthWorld);
		
        //calculate deltaY
		float dy = y - transform.position.y;
		v.y = Mathf.RoundToInt(0.5f * _heightPixel + dy * _heightPixel / _heightWorld);

        return v;
    }

    //remove function from corresponding event
    private void OnDisable()
    {
        Bomb.OnDestroyGround -= DestroyGround;
    }
}