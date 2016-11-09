using UnityEngine;
using System.Collections;

public class SpriteGenerator : MonoBehaviour {

    public Texture2D Texture;
    public PolygonCollider2D Collider;
    private Sprite sprite;
    public SpriteRenderer SRenderer;

	// Use this for initialization
	void Start () {
        sprite = Sprite.Create(Texture, new Rect(10, 10, 15, 20), Vector2.zero, 1);
        SRenderer.sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
