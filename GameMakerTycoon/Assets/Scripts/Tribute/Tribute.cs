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
		public int intelligence;
		public int aggressiveness;
		public int resourcefulness;
		public int cooperativeness;
		public int courage;
		public int charisma;
		public int hungerBAScore;
		public int maxAttribScore;
	};

	public Transform mItemUseAnchorPoint;

	private string mName;
	private int mTeamNumber = -1;
	private int mDistrict;
	private Gender mGender;

	private AttributeStats mAttributeStats;

	private int mMaxHungerLevel;
	private int mCurrentHungerLevel;
	private int mMaxThirstLevel;
	private int mCurrentThirstLevel;


	private Combatant mCombatant;
	private Mover mMover;

	private Inventory mInventory;

	public string Name
	{
		get{ return mName; }
		set{ mName = value;	}
	}

	public int District
	{
		get{ return mDistrict; }
		set{ mDistrict = value; }
	}

	public Gender TheGender
	{
		get{ return mGender; }
		set{ mGender = value; }
	}

	public AttributeStats AttribStats
	{
		get{ return mAttributeStats; }
		set{ mAttributeStats = value; }
	}
	
	public int MaxHungerLevel
	{
		get	{ return mMaxHungerLevel; }
		set	{ mMaxHungerLevel = value; }
	}

	public int CurrentHungerLevel
	{
		get { return mCurrentHungerLevel; }
	}

	public int MaxThirstLevel
	{
		get	{ return mMaxThirstLevel; }
		set	{ mMaxThirstLevel = value; }
	}
	
	public int CurrentThirstLevel
	{
		get { return mCurrentThirstLevel; }
	}


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

	public void ApplyItemEffects( Item.ItemEffect effects )
	{
		mCurrentHungerLevel += effects.modToHungerLevel;
		mCurrentThirstLevel += effects.modToThirstLevel;
	}

	public Inventory TheInventory
	{
		get{ return mInventory; }
	}
	

	// Use this for initialization
	void Start () {
		mCombatant = GetComponent<Combatant>();
		mMover = GetComponent<Mover>();
		mInventory = new Inventory( this );
	}
	
	// Update is called once per frame
	void Update () {
		if( mCombatant.CurrentHealth <= 0 )
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
