using UnityEngine;
using System.Collections;

public class UI_TileSelected : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject myGroup;

	// Use this for initialization
	void Start () {
		//GameObject obj = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		//obj.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnActivate(bool state)
	{
		if (state)
		{
			Debug.Log("Tile Selected: " + name);
			myGroup.GetComponent<UI_TileScript>().SetTile(tilePrefab);

		}
		else
		{

		}
	}
}
