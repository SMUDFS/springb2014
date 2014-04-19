using UnityEngine;
using System.Collections;

public class AccessableObject : MonoBehaviour {

	public bool mIsBeingAccessed = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateAccessableObject();
	}

	public void UpdateAccessableObject()
	{
		if(Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
			mIsBeingAccessed = false;
	}
}
