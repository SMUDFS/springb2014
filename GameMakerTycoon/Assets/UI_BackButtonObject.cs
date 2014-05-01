using UnityEngine;
using System.Collections;

public class UI_BackButtonObject : MonoBehaviour {

	public MapObjectList list;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		GameObject.Find("Table").GetComponent<HungerGamesMap>().mIsBeingAccessed = false;
		list.OnSelectionChange("");
	}
}
