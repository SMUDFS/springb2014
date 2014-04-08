using UnityEngine;
using System.Collections;

public class WeaponBlueprint
{
	private string mName;
	private string mType;
	private Combatant.AttackStats mAttackStats;
	private int mAmmo;
	private string mPrefabName;

	public Combatant.AttackStats AttackStats
	{
		get
		{
			return mAttackStats;
		}
		set
		{
			mAttackStats = value;
		}
	}

	public int Ammo
	{
		get
		{
			return mAmmo;
		}
		set
		{
			mAmmo = value;
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

	public string PrefabName
	{
		get
		{
			return mPrefabName;
		}
		set
		{
			mPrefabName = value;
		}
	}

	public WeaponBlueprint ( string type )
	{
		mType = type;
	}

	virtual public GameObject SpawnWeapon( Vector3 pos )
	{
		return null;
	}
}

