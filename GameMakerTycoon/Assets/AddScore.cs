using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

	public GameObject nguiScorePrefab;
	private int id;

	void CreateScore( string tributeName )
	{
		GameObject newScore = NGUITools.AddChild (GameObject.Find ("ScoreGrid"), nguiScorePrefab);
		GameObject.Find ("ScoreGrid").GetComponent<UIGrid> ().Reposition ();
		newScore.transform.FindChild ("NameLabel").GetComponent<UILabel> ().text = tributeName + id;
		newScore.transform.FindChild ("RankLabel").GetComponent<UILabel> ().text = (++id).ToString();
		Debug.Log ("Made a new score");
	}

	void OnClick()
	{
		CreateScore ("Name");
	}
}
