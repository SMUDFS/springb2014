using UnityEngine;
using System.Collections;

public class TributeSpawner : MonoBehaviour {

	public int mDistrict = 1;
	public Tribute.Gender mGender;

	public bool mShouldSpawn = true;

	// Use this for initialization
	void Start () {

	}

	void Update()
	{
		if( mShouldSpawn )
		{
			TributeLoader loader = FindObjectOfType<TributeLoader>();
			if( loader != null )
			{
				GameObject tribute = loader.SpawnTribute( mDistrict, mGender );
				if( tribute != null )
				{
					tribute.transform.position = gameObject.transform.position;
					Debug.Log( "Tribute Spawned: Name: " + tribute.GetComponent<Tribute>().Name +" District: " + mDistrict );
				}
			}
			mShouldSpawn = false;
		}
	}

}
