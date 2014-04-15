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

	public bool mTableActivated = false;

	//Private
	private int mNumTilesX = 25;
	private int mNumTilesZ = 25;

	private Vector3 mMinTilesPos;
	private Vector3 mMaxTilesPos;

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

		Resize(10, 10);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetTableActivated(bool val)
	{

	}

	public void ChangeTile(ref GameObject tilePrefab)
	{
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(cameraRay);

		if(hits != null)
		{
			Vector3 hitPos = hits[0].point;
			int gridX = Mathf.FloorToInt((hitPos.x - mMinTilesPos.x) / mTileDims.x);
			int gridZ = Mathf.FloorToInt((hitPos.z - mMinTilesPos.z) / mTileDims.y);

			if(gridX >= 0 && gridX < mNumTilesX && gridZ >= 0 && gridZ < mNumTilesZ)
			{
				int tileIndex = GetTileIndex(gridX, gridZ);

				if (mTiles[tileIndex].name != string.Format("{0}(Clone)", tilePrefab.name))
				{
					Vector3 pos = mTiles[tileIndex].transform.position;
					Destroy(mTiles[tileIndex]);
					mTiles[tileIndex] = Instantiate(tilePrefab) as GameObject;
					mTiles[tileIndex].transform.position = pos;
				}
			}
		}
	}

	public void Resize(int numTilesX, int numTilesZ)
	{
		mNumTilesX = numTilesX;
		mNumTilesZ = numTilesZ;

		float yScale = transform.localScale.y;

		transform.localScale = new Vector3(mNumTilesX * mTileDims.x, yScale, mNumTilesZ * mTileDims.y);

		mMinTilesPos = mTableMiddlePos;
		mMinTilesPos.x -= numTilesX * mTileDims.x * 0.5f;
		mMinTilesPos.z -= numTilesZ * mTileDims.y * 0.5f;

		mMaxTilesPos = mMinTilesPos;
		mMaxTilesPos.x += numTilesX * mTileDims.x;
		mMaxTilesPos.z += numTilesZ * mTileDims.y;

		foreach(GameObject tile in mTiles)
			Destroy(tile);

		mTiles.Clear();
		for(int i = 0; i < mNumTilesX; ++i)
			for(int j = 0; j < mNumTilesZ; ++j)
		{
			Vector3 position = mMinTilesPos;
			position.x += i * mTileDims.x + 0.5f;
			position.y = mTableMiddlePos.y;
			position.z += j * mTileDims.y + 0.5f;

			GameObject temp = Instantiate(mPossibleTiles[0].tilePrefab) as GameObject;
			temp.transform.position = position;
			mTiles.Add(temp);
		}
	}

	private int GetTileIndex(int x, int z)
	{
		return x * mNumTilesZ + z;
	}
}
