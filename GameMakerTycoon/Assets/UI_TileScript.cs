using UnityEngine;
using System.Collections;

public class UI_TileScript : MonoBehaviour {
	
	private HungerGamesMap mMap;
	private GameObject mTile;
	
	// Use this for initialization
	void Start ()
	{
		mMap = GameObject.Find("Table").GetComponent<HungerGamesMap>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleInput();
	}
	
	private void HandleInput()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			mMap.ChangeTile(ref mTile);
		}
	}

	public void SetTile(GameObject tile)
	{
		mTile = tile;
	}
}
