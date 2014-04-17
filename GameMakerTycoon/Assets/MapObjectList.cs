using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapObjectList : MonoBehaviour {
	
	public List<GameObject> RootObjects = new List<GameObject>();
	private Dictionary<string, int> mNameToIndex = new Dictionary<string, int>();

	// Use this for initialization
	void Start ()
	{
		int index = 0;
		foreach(GameObject obj in RootObjects)
		{
			if (obj != null)
				mNameToIndex[obj.tag] = index;
			++index;
		}
	}

	void OnSelectionChange(string selectedItem)
	{
		// Try to get the group with the selected tag
		GameObject activeRoot = null;
		int index = 0;
		if (mNameToIndex.TryGetValue(selectedItem, out index))
			activeRoot = RootObjects[index];

		// Hide all the stuff
		foreach(GameObject obj in RootObjects)
		{
			if (obj == null) continue;
			obj.SetActive(false);
			foreach(Transform child in obj.transform)
			{
				child.gameObject.SetActive(true);
			}
		}
		// Show the selected stuff
		if (activeRoot != null)
		{
			activeRoot.SetActive(true);
			foreach(Transform child in activeRoot.transform)
			{
				child.gameObject.SetActive(true);
			}
		}
	}
}
