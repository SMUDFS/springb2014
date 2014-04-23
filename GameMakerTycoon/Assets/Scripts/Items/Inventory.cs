using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	Tribute mOwner;
	private List<Item> mItems = new List<Item>();


	public Inventory( Tribute owner )
	{
		mOwner = owner;
	}

	public void UseItem( int idx, Tribute targetTribute )
	{
		if( idx >= 0 && idx < mItems.Count )
			mItems[ idx ].UseItem( targetTribute );
	}
	
	public void PickupItem( Item item )
	{
		mItems.Add( item );
		item.PickupItem( mOwner );
	}

	public void DropItem( int idx )
	{
		if( idx >= 0 && idx < mItems.Count )
		{
			Item item = mItems[ idx ];
			item.gameObject.transform.position = mOwner.transform.position - 2.0f * mOwner.gameObject.transform.forward * mOwner.gameObject.collider.bounds.extents.sqrMagnitude;
			item.DropItem();
			mItems.RemoveAt( idx );
		}
	}

	public Item.ItemEffect GetItemEffect( int idx )
	{
		Item.ItemEffect effect = new Item.ItemEffect();
		if( idx >= 0 && idx < mItems.Count )
		{
			effect = mItems[ idx ].Effect;
		}
		return effect;
	}

	public int Count
	{
		get { return mItems.Count; }
	}
}
