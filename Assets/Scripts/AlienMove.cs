using UnityEngine;
using System.Collections;

public class AlienMove : MonoBehaviour {

	[SerializeField]
	private float alienSpeed = 2.0f;
	private SpriteRenderer alienSpriteRender;

	// Use this for initialization
	void Start () 
	{
		alienSpriteRender = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.x < -400)
		{
			Destroy(gameObject);
		}
		else
		{
			transform.position += Vector3.left * alienSpeed;
		}
	
	}

	void OnTriggerEnter2D(Collider2D objectHit)
	{
		//print(objectHit.name);
		if (objectHit.name.StartsWith("Cheeseburger"))
		{
			Destroy (objectHit.gameObject);
			Destroy(gameObject);
		}
		else if (objectHit.name.Equals("Player"))
		{
			print("Ouch!");
		}
	
	}
}
