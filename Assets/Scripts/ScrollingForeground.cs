using UnityEngine;
using System.Collections;

public class ScrollingForeground : MonoBehaviour {

	public float scrollSpeed;
	public Material midgroundMaterial2;
	public float numOfRepeats;
	
	private Vector2 savedOffset;
	private Renderer textureRender;
	private float timeAccumulator = 0.0f;
	private bool isSecondMaterial = false;
	private bool swapMaterials = false;
	// Use this for initialization
	void Start () 
	{
		textureRender = gameObject.GetComponent<Renderer>();
		savedOffset = textureRender.sharedMaterial.GetTextureOffset("_MainTex");
		textureRender.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeAccumulator += Time.deltaTime;
		float x = timeAccumulator * scrollSpeed;
		Vector2 newOffset = new Vector2(x, savedOffset.y);
		textureRender.sharedMaterial.SetTextureOffset("_MainTex", newOffset);
		if(x > numOfRepeats && !isSecondMaterial)
		{
			timeAccumulator = 0.0f;
			swapMaterials = true;
		}

		if(swapMaterials)
		{
			textureRender.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
			textureRender.sharedMaterial = midgroundMaterial2;
			savedOffset = textureRender.sharedMaterial.GetTextureOffset("_MainTex");
			swapMaterials = false;
			isSecondMaterial = true;
		}

		if(!(x > 0.99f && isSecondMaterial)) //stop moving 2nd texture after it reaches the end
		{
			textureRender.sharedMaterial.SetTextureOffset("_MainTex", newOffset);
		}

		
	}
	
	void OnDisable()
	{
		textureRender.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}
}
