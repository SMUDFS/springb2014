using UnityEngine;
using System.Collections;

public class UI_TileScript : MonoBehaviour {

	public int startFunds = 1000;

	private HungerGamesMap mMap;
	private GameObject mTile;
	private TileInfo mTileInfo;
	private int mCurrentFunds;
	private static GameObject mFundsLabel = null;
	private static GameObject mCostLabel = null;
	private static GameObject mNameLabel = null;
	private static GameObject mDescription = null;
	private static GameObject mFundsEffectLabel = null;
	
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
		if (mFundsLabel == null)
		{
			mFundsLabel = GameObject.Find("FundsLabel");
			mFundsLabel.GetComponent<UILabel>().text = "Funds: $" + startFunds;
			mCurrentFunds = startFunds;
		}
		if ( mFundsEffectLabel == null )
		{
			mFundsEffectLabel = GameObject.Find("FundsBuyEffect");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleInput();
	}

	private bool IsFundsAvailable()
	{
		return mCurrentFunds - mTileInfo.mCost >= 0;
	}

	private void ApplyPurchase()
	{
		mCurrentFunds -= mTileInfo.mCost;
		mFundsLabel.GetComponent<UILabel>().text = "Funds: $" + mCurrentFunds;

		if ( mTileInfo.mCost > 0 )
		{
			mFundsEffectLabel.GetComponent<UILabel>().text = "$-" + mTileInfo.mCost;
			mFundsEffectLabel.GetComponent<UIPanel>().alpha = 1;
			Vector3 pos = mFundsEffectLabel.transform.localPosition;
			pos.y = 7.18f;
		//	pos.y = 45.0f;
			mFundsEffectLabel.transform.localPosition = pos;
			UITweener.GetTweenOfGroup( mFundsEffectLabel, 0, false ).Play( true );
		}
	}
	
	private void HandleInput()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			if ( mTile == null )
				;
			else if ( mTile.tag == "Tile" )
			{
				if ( IsFundsAvailable() )
				{
					if ( mMap.ChangeTile(ref mTile) )
					{
						Debug.Log("Placing Tile: " + mTile.name);
						ApplyPurchase();
					}

				}
			}
			else if ( mTile.tag == "Trap" )
			{
				Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll(cameraRay);
				foreach(RaycastHit hit in hits)
				{
					GameObject tile = hit.collider.gameObject;
					if ( tile != null && tile.tag == "Tile" && IsFundsAvailable() )
					{
						Debug.Log("Placing Trap: " + mTile.name);
						ApplyPurchase();
						Vector3 spawnLoc = hit.point;
						spawnLoc.y += 0.5f;
						Instantiate( mTile, spawnLoc, tile.transform.rotation );
					}
				}
			}
		}
	}

	public void SetTile(GameObject tile)
	{
		mTile = tile;
		if ( mTile != null )
		{
			Debug.Log("Tile Set: " + mTile.name);

			TileInfo info = mTile.GetComponent<TileInfo>();
			if ( info != null )
			{
				mTileInfo = info;
				mCostLabel.GetComponent<UILabel>().text = "Cost: $" + info.mCost;
				mNameLabel.GetComponent<UILabel>().text = "Name: " + info.mName;
				mDescription.GetComponent<UILabel>().text = "Info\n" + info.mDescription;
			}
		}
		else
		{
			Debug.Log ("Tile Set: NULL");
		}
	}
}
