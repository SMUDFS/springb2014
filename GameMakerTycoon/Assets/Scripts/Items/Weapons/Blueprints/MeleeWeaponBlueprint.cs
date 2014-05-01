using UnityEngine;
using System;
using RAIN.Core;
using RAIN.Entities;

public class MeleeWeaponBlueprint : WeaponBlueprint
{
	public MeleeWeaponBlueprint ()
		: base( "melee" )
	{
	}

	override public GameObject SpawnWeapon( Vector3 pos )
	{
		GameObject weapon = GameObject.Instantiate( Resources.Load( "Items/Weapons/" + this.PrefabName, typeof(GameObject) ) ) as GameObject;
		weapon.transform.position = pos;
		MeleeWeapon weaponComp = weapon.AddComponent<MeleeWeapon>();
		weaponComp.AttackStats = this.AttackStats;
		weaponComp.NumberOfUses = this.Ammo;
		weaponComp.Name = this.Name;

		GameObject entity = GameObject.Instantiate( Resources.Load( "Items/ItemEntity", typeof(GameObject) ) ) as GameObject;
		entity.transform.parent = weapon.transform;
		
		EntityRig entityRig = entity.GetComponent<EntityRig>();
		entityRig.Entity.Form = weapon;

		return weapon;
	}
}

