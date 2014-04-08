using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Combatant))]
[RequireComponent (typeof(Mover))]
[RequireComponent (typeof(Rigidbody))]
public class Tribute : MonoBehaviour {

	public enum Gender
	{
		MALE,
		FEMALE
	};

	public class AttributeStats
	{
		int intelligence;
		int aggressiveness;
		int resourcefulness;
		int cooperativeness;
		int courage;
		int charisma;
		int hungerBAScore;
	};

	public Transform mItemUseAnchorPoint;

	private string mName;
	private int mId;
	private int mTeamNumber = -1;
	private int mDistrict;
	private Gender mGender;

	private AttributeStats mAttributeStats;

	private int mHungerLevel;
	private int mThirstLevel;

	private Combatant mCombatant;
	private Mover mMover;

	private Inventory mInventory;

	public void UseItem( int idx, Tribute targetTribute )
	{
		mInventory.UseItem( idx, targetTribute );
	}

	public void PickupItem( Item item )
	{
		mInventory.PickupItem( item );
	}

	public void DropItem( int idx )
	{
		mInventory.DropItem( idx );
	}

	// Use this for initialization
	void Start () {
		mCombatant = GetComponent<Combatant>();
		mMover = GetComponent<Mover>();
		mInventory = new Inventory( this );
	}
	
	// Update is called once per frame
	void Update () {
		if( mCombatant.Health <= 0 )
		{
			if( mMover.mDebugSphere != null )
				Destroy( mMover.mDebugSphere );
			Destroy( gameObject );
		}
	}

	void OnCollisionEnter( Collision collision )
	{
		Item item = collision.gameObject.GetComponent<Item>();
		if( item != null )
		{
			PickupItem( item );
		}
	}
}
