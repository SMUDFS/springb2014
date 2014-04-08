//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;

public class RangedWeaponBlueprint : WeaponBlueprint
{
	private string mProjPrefabName;
	private float mProjSpeed;

	public string ProjPrefabName
	{
		get
		{
			return mProjPrefabName;
		}
		set
		{
			mProjPrefabName = value;
		}
	}

	public float ProjSpeed
	{
		get
		{
			return mProjSpeed;
		}
		set
		{
			mProjSpeed = value;
		}
	}

	public RangedWeaponBlueprint ()
		:	base( "ranged" )
	{}

	override public GameObject SpawnWeapon( Vector3 pos )
	{
		Debug.Log( "attempting to spawn ranged weapon" );
		GameObject weapon = GameObject.Instantiate( Resources.Load( "Items/Weapons/" + this.PrefabName, typeof(GameObject) ), pos, Quaternion.identity ) as GameObject;
		
		RangeWeapon weaponComp = weapon.AddComponent<RangeWeapon>();
		weaponComp.AttackStats = this.AttackStats;
		weaponComp.NumberOfUses = this.Ammo;
		weaponComp.Name = this.Name;
		weaponComp.mProjectilePrefab = Resources.Load( "Items/Weapons/" + this.ProjPrefabName, typeof( GameObject ) ) as GameObject;
		weaponComp.mProjectileSpeed = mProjSpeed;
		return weapon;
	}
}

