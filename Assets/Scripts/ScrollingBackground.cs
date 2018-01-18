using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;

	// Use this for initialization
	void Start () 
	{
		savedOffset = gameObject.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 newOffset = new Vector2(Time.time * scrollSpeed, savedOffset.y);
		if (newOffset.x < 0.67)
		{
		gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", newOffset);
		}
	
	}

	void OnDisable()
	{
		gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}
}
