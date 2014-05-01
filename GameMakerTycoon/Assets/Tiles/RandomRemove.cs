using UnityEngine;
using System.Collections;

public class RandomRemove : MonoBehaviour {

	public float mChance = 0.5f;

	// Use this for initialization
	void Start() 
	{
		if(Random.Range(0.0f, 1.0f) < mChance)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
