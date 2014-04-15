using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CachedTile
{
	public string tileName;
	public GameObject tilePrefab;
}

public class HungerGamesMap : MonoBehaviour {
	//Public
	public Vector2 mTileDims;

	public CachedTile[] mPossibleTiles;

	//Private
	private int mNumTilesX = 25;
	private int mNumTilesZ = 25;

	private Vector3 mTableMiddlePos;

	private BoxCollider mTableCollider;

	private List<GameObject> mTiles = new List<GameObject>();

	// Use this for initialization
	void Start()
	{
		mTableCollider = GetComponent<BoxCollider>() as BoxCollider;
		if(!mTableCollider)
			Debug.LogError("NO Box Collider for Table!");
		else
		{
			Vector3 size = mTableCollider.size;
			size.Scale(mTableCollider.transform.localScale);
			mTableMiddlePos = mTableCollider.transform.position + mTableCollider.center;
			mTableMiddlePos.y += size.y * 0.5f + 0.1f;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Resize(int numTilesX, int numTilesZ)
	{
		mNumTilesX = numTilesX;
		mNumTilesZ = numTilesZ;

		float yScale = transform.localScale.y;

		transform.localScale = new Vector3(mNumTilesX * mTileDims.x, yScale, mNumTilesZ * mTileDims.y);

		Vector3 minPos = mTableMiddlePos;
		minPos.x -= numTilesX * mTileDims.x * 0.5f;
		minPos.z -= numTilesZ * mTileDims.y * 0.5f;

		foreach(GameObject tile in mTiles)
			Destroy(tile);

		mTiles.Clear();
		for(int i = 0; i < mNumTilesX; ++i)
			for(int j = 0; j < mNumTilesZ; ++j)
		{
			Vector3 position = minPos;
			position.x += i * mTileDims.x + 0.5f;
			position.y = mTableMiddlePos.y;
			position.z += j * mTileDims.y + 0.5f;

			GameObject temp = Instantiate(mPossibleTiles[0].tilePrefab) as GameObject;
			temp.transform.position = position;
			mTiles.Add(temp);
		}
	}
}
