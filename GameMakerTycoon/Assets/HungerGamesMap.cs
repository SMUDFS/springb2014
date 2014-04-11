using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HungerGamesMap : MonoBehaviour {

	public Vector2 mTileDims;

	private int mNumTilesX = 25;
	private int mNumTilesZ = 25;

	private Vector3 mTableMiddlePos;

	private BoxCollider mTableCollider;

	private List<GameObject> mTiles;

	private GameObject mCachedForestTile;

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

		mTiles = new List<GameObject>();

		mCachedForestTile = Instantiate(Resources.Load("Tiles/Tile_Grass")) as GameObject;
		if(!mCachedForestTile)
			Debug.Log("FG");

		Resize(15, 15);
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

		mTiles.Clear();
		for(int i = 0; i < mNumTilesX; ++i)
			for(int j = 0; j < mNumTilesZ; ++j)
		{
			Vector3 position = minPos;
			position.x += i * mTileDims.x + 0.5f;
			position.y = mTableMiddlePos.y;
			position.z += j * mTileDims.y + 0.5f;

			GameObject temp = Instantiate(mCachedForestTile) as GameObject;
			temp.transform.position = position;
			mTiles.Add(temp);
		}
	}
}
