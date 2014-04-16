using UnityEngine;
using System.Collections;

public class UI_CreateLevelScript : MonoBehaviour {

	public GameObject CreatePanel;
	public GameObject DesignPanel;
	private bool mWasCreated = false;

	// Use this for initialization
	void Start () {
		GameObject.Find("First Person Controller").GetComponent<ActivateStuffScript>().mGUIStartFunc = StartGUIEvent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGUIEvent(GameObject accessedObject)
	{
		Debug.Log ("Object was used: " + accessedObject.name);
		if (accessedObject.name == "Table")
		{
			if (mWasCreated)
			{
				DesignPanel.SetActive(true);
				DesignPanel.GetComponent<TweenAlpha>().Play(true);
				DesignPanel.GetComponent<TweenScale>().Play(true);
			}
			else
			{
				CreatePanel.SetActive(true);
				mWasCreated = true;
			}
		}
	}
}
