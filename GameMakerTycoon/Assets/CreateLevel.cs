using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateLevel : MonoBehaviour {

	private SortedList<string, Vector2> mSizeTable = new SortedList<string, Vector2>();

	void Start()
	{
		mSizeTable.Add("Turkish Hovel", new Vector2(5,5));
		mSizeTable.Add("Small", new Vector2(10,10));
		mSizeTable.Add("Large", new Vector2(15,15));
		mSizeTable.Add("Massive", new Vector2(20,20));
		mSizeTable.Add("Kyles Mom", new Vector2(30,30));
	}

	void OnClick()
	{
		string sizeName = GameObject.Find ("SizeList").GetComponent<UIPopupList>().selection;
		string terrainName = GameObject.Find ("TerrainTypeList").GetComponent<UIPopupList>().selection;

		// Going to want to call a function to creat the level
		Vector2 size = mSizeTable[sizeName];
		GameObject.Find("Table").GetComponent<HungerGamesMap>().Resize((int)size.x, (int)size.y, "Desert");

		Debug.Log ("Creating level: size=\"" + sizeName + "\"" + "terrain=\"" + terrainName + "\"" );
	}
}
