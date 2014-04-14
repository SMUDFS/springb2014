using UnityEngine;
using System.Collections;

public class CreateLevel : MonoBehaviour {

	void OnClick()
	{
		string sizeName = GameObject.Find ("SizeList").GetComponent<UIPopupList>().selection;
		string terrainName = GameObject.Find ("TerrainTypeList").GetComponent<UIPopupList>().selection;

		// Going to want to call a function to creat the level

		GameObject.Find("Table").GetComponent<HungerGamesMap>().Resize(5,5);

		Debug.Log ("Creating level: size=\"" + sizeName + "\"" + "terrain=\"" + terrainName + "\"" );
	}
}
