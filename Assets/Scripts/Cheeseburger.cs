using UnityEngine;
using System.Collections;

public class Cheeseburger : MonoBehaviour {

	[SerializeField]
	private float burgerSpeed = 2.0f;
	private SpriteRenderer burgerSpriteRender;
	// Use this for initialization
	void Start () {
	
		burgerSpriteRender = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (burgerSpriteRender.isVisible)
		{
			gameObject.transform.position += Vector3.right * burgerSpeed;
		}

	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
