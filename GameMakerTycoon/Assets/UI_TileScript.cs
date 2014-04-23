using UnityEngine;
using System.Collections;

public class UI_TileScript : MonoBehaviour {
	
	private HungerGamesMap mMap;
	private GameObject mTile;
	private static GameObject mCostLabel = null;
	private static GameObject mNameLabel = null;
	private static GameObject mDescription = null;
	
	// Use this for initialization
	void Start ()
	{
		mMap = GameObject.Find("Table").GetComponent<HungerGamesMap>();
		if (mCostLabel == null)
		{
			mCostLabel = GameObject.Find("CostLabel");
		}
		if (mNameLabel == null)
		{
			mNameLabel = GameObject.Find("NameLabel");
		}
		if (mDescription == null)
		{
			mDescription = GameObject.Find("Description");
		}
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
		TileInfo info = tile.GetComponent<TileInfo>();
		mCostLabel.GetComponent<UILabel>().text = "Cost: " + info.mCost;
		mNameLabel.GetComponent<UILabel>().text = "Name: " + info.mName;
		mDescription.GetComponent<UILabel>().text = "Info" + info.mDescription;
	}
}
