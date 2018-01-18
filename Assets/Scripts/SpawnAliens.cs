using UnityEngine;
using System.Collections;

public class SpawnAliens : MonoBehaviour {

	[SerializeField]
	private float spawnTimer;
	private float timer;
	public GameObject alienPrefab;
	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if(timer > spawnTimer)
		{
			timer = 0.0f;
			Instantiate(alienPrefab);
		}
	}


}
