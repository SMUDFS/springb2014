using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public class ItemEffect
	{
		public int modToHealth = 0;
		public int modToHungerLevel = 0;
		public int modToThirstLevel = 0;
	};

	public string mName;
	protected Tribute mItemOwner;
	private int mNumUses = -1; //This means infinite

	protected ItemEffect mItemEffect =  new ItemEffect();


	public ItemEffect Effect
	{
		get
		{
			return mItemEffect;
		}
	}

	public int NumberOfUses
	{
		get
		{
			return mNumUses;
		}
		set
		{
			mNumUses = value;
		}
	}

	public string Name
	{
		get
		{
			return mName;
		}
		set
		{
			mName = value;
		}
	}

	public void PickupItem( Tribute newOwner )
	{
		mItemOwner = newOwner;
		Renderer renderer = GetComponent<Renderer>();
		renderer.enabled = false;
		enabled = false;
		collider.enabled = false;
	}

	public void DropItem()
	{
		mItemOwner = null;
		Renderer renderer = GetComponent<Renderer>();
		renderer.enabled = true;
		enabled = true;
		collider.enabled = true;
	}

	//Should return true if the item was succesfully used
	public bool UseItem( Tribute targetTribute )
	{
		bool success = false;
		if( mNumUses > 0 || mNumUses == -1 )
		{
			success = UseItemSubClass( targetTribute );
			if( mNumUses > 0 && success )
				--mNumUses;
		}

		return success;
	}

	protected virtual bool UseItemSubClass( Tribute targetTribute )
	{
		return false;
	}

	void Update()
	{

	}


}
