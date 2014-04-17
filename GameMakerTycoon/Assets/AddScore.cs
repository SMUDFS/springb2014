using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

	public GameObject nguiScorePrefab;
	private int id;

	void CreateScoreEntry( Tribute tribute )
	{
		GameObject newScore = NGUITools.AddChild (GameObject.Find ("ScoreGrid"), nguiScorePrefab);
		GameObject.Find ("ScoreGrid").GetComponent<UIGrid> ().Reposition ();
		newScore.transform.FindChild ("NameLabel").GetComponent<UILabel> ().text = tribute.name;
		newScore.transform.FindChild ("RankLabel").GetComponent<UILabel> ().text = (++id).ToString();
		Debug.Log ("Made a new score");
	}

	void OnClick()
	{

	}
}
