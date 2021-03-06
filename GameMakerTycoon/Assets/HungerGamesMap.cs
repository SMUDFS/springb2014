﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CachedTile
{
	public string tileName;
	public GameObject tilePrefab;
}

public class HungerGamesMap : AccessableObject {
	//Public
	public CachedTile[] mPossibleTiles;

	//Private
	private int mNumTilesX = 15;
	private int mNumTilesZ = 15;

	private Vector3 mMinTilesPos;
	private Vector3 mMaxTilesPos;

	private Vector3 mTableMiddlePos;

	private BoxCollider mTableCollider;

	private List<GameObject> mTiles = new List<GameObject>();

	public GameObject mNavMeshObj;
	private RAIN.Navigation.NavMesh.NavMeshRig mNavMeshRig;
	private RAIN.Navigation.NavMesh.NavMesh mNavMesh;

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

		mNavMeshRig = mNavMeshObj.GetComponent<RAIN.Navigation.NavMesh.NavMeshRig>();

		if(!mNavMeshRig)
			Debug.LogError("Missing NavMESH!");

		mNavMesh = mNavMeshRig.NavMesh;

		Resize(mNumTilesX, mNumTilesZ, "Forest");
		RegeneratePathing();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateAccessableObject();

		if(Input.GetMouseButtonDown(0) && mIsBeingAccessed)
		{
			Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(cameraRay);
			foreach(RaycastHit hit in hits)
			{
				Trap trap = hit.collider.gameObject.GetComponent<Trap>();
				if(trap != null)
					trap.Trigger();
			}
		}
	}


	///////////////////////////////////////////////////////////////
	/// Map Editing Functions
	///////////////////////////////////////////////////////////////
	public void RegeneratePathing()
	{
		mNavMesh.StartCreatingContours(mNavMeshRig, 4);
		while(mNavMesh.Creating)
		{
			mNavMesh.CreateContours();
		}
	}

	public bool ChangeTile(ref GameObject tilePrefab)
	{
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(cameraRay);

		if(hits != null)
		{
			Vector3 hitPos = hits[0].point;
			int gridX = Mathf.FloorToInt(hitPos.x - mMinTilesPos.x);
			int gridZ = Mathf.FloorToInt(hitPos.z - mMinTilesPos.z);

			if(gridX >= 0 && gridX < mNumTilesX && gridZ >= 0 && gridZ < mNumTilesZ)
			{
				int tileIndex = GetTileIndex(gridX, gridZ);

				if (mTiles[tileIndex].name != string.Format("{0}(Clone)", tilePrefab.name))
				{
					TileInfo oldInfo = mTiles[tileIndex].GetComponent<TileInfo>();
					TileInfo newInfo = tilePrefab.GetComponent<TileInfo>();
					if(oldInfo.mBlocksPath && !newInfo.mBlocksPath)
					{
						//remove blockage
					}
					else if(!oldInfo.mBlocksPath && newInfo.mBlocksPath)
					{
						//Add Blockage
					}

					Vector3 pos = mTiles[tileIndex].transform.position;
					Destroy(mTiles[tileIndex]);
					mTiles[tileIndex] = Instantiate(tilePrefab) as GameObject;
					mTiles[tileIndex].transform.position = pos;

					return true;
				}
			}
		}
		return false;
	}

	public void Resize(int numTilesX, int numTilesZ, string name)
	{
		mNumTilesX = numTilesX;
		mNumTilesZ = numTilesZ;

		float yScale = transform.localScale.y;

		transform.localScale = new Vector3(mNumTilesX, yScale, mNumTilesZ);

		mMinTilesPos = mTableMiddlePos;
		mMinTilesPos.x -= numTilesX * 0.5f;
		mMinTilesPos.z -= numTilesZ * 0.5f;

		mMaxTilesPos = mMinTilesPos;
		mMaxTilesPos.x += numTilesX;
		mMaxTilesPos.z += numTilesZ;

		foreach(GameObject tile in mTiles)
			Destroy(tile);

		mTiles.Clear();

		GameObject tilePrefab = GetTilePrefabByName(name);
		for(int i = 0; i < mNumTilesX; ++i)
			for(int j = 0; j < mNumTilesZ; ++j)
		{
			Vector3 position = mMinTilesPos;
			position.x += i + 0.5f;
			position.y = mTableMiddlePos.y;
			position.z += j + 0.5f;

			GameObject temp = Instantiate(tilePrefab) as GameObject;
			temp.transform.position = position;

			mTiles.Add(temp);
		}

		mNavMeshRig.transform.localScale = new Vector3(transform.localScale.x, 0.05f, transform.localScale.z);
                                
	}

	private GameObject GetTilePrefabByName(string name)
	{
		foreach(CachedTile cacheTile in mPossibleTiles)
		{
			if(cacheTile.tileName == name)
			{
				return cacheTile.tilePrefab;
			}
		}

		return null;
	}

	private int GetTileIndex(int x, int z)
	{
		return x * mNumTilesZ + z;
	}
}
