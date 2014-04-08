using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class WeaponLoader : MonoBehaviour {

	public TextAsset mWeaponXML;

	private Dictionary<string,WeaponBlueprint> mBlueprints = new Dictionary<string, WeaponBlueprint>();

	void Start()
	{
		LoadWeapons();
	}

	public GameObject SpawnWeapon( string name, Vector3 pos )
	{
		WeaponBlueprint blueprint = null;
		mBlueprints.TryGetValue( name, out blueprint );

		if( blueprint != null )
		{
			return blueprint.SpawnWeapon( pos );
		}
		else
		{
			return null;
		}
	}

	public void LoadWeapons()
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml( mWeaponXML.text );


		XmlNodeList weaponNodes = doc.DocumentElement.SelectNodes( "/Weapons/Weapon" );

		foreach( XmlNode weaponNode in weaponNodes )
		{
			string type = weaponNode.Attributes[ "type" ].Value;
			
			Combatant.AttackStats attackStats = new Combatant.AttackStats();
			string power = weaponNode.SelectSingleNode( "Power" ).InnerText;
			string speed = weaponNode.SelectSingleNode( "Speed" ).InnerText;
			string range = weaponNode.SelectSingleNode( "Range" ).InnerText;
			string ammoStr = weaponNode.Attributes[ "ammo" ].Value;
			
			if( !int.TryParse( power, out attackStats.power ) )
				attackStats.power = 1;
			if( !float.TryParse( speed, out attackStats.speed ) )
				attackStats.speed = 1.0f;
			if( !float.TryParse( range, out attackStats.range ) )
				attackStats.range = 1.0f;

			int ammo = 0;
			int.TryParse( ammoStr, out ammo );

			if( type == "ranged" )
			{
				LoadRangedWeapon( weaponNode, attackStats, ammo );
			}
			else if( type == "melee" )
			{
				LoadMeleeWeapon( weaponNode, attackStats, ammo );
			}

		}

	}

	private void LoadRangedWeapon( XmlNode weaponNode, Combatant.AttackStats attackStats, int ammo )
	{
		RangedWeaponBlueprint blueprint = new RangedWeaponBlueprint();
		blueprint.ProjPrefabName = weaponNode.Attributes[ "projName" ].Value;
		blueprint.PrefabName = weaponNode.Attributes[ "prefabName" ].Value;
		blueprint.AttackStats = attackStats;
		blueprint.Ammo = ammo;
		blueprint.Name = weaponNode.Attributes[ "name" ].Value;

		string projSpeedStr = weaponNode.SelectSingleNode( "ProjSpeed" ).InnerText;
		float projSpeed = 0.0f;
		float.TryParse( projSpeedStr, out projSpeed );

		blueprint.ProjSpeed = projSpeed;
		mBlueprints.Add( blueprint.Name, blueprint );

		Debug.Log( "Weapon " + blueprint.Name + " loaded" );
	}

	private void LoadMeleeWeapon( XmlNode weaponNode, Combatant.AttackStats attackStats, int ammo )
	{
		MeleeWeaponBlueprint blueprint = new MeleeWeaponBlueprint();
		blueprint.AttackStats = attackStats;
		blueprint.PrefabName = weaponNode.Attributes[ "prefabName" ].Value;
		blueprint.Ammo = ammo;
		blueprint.Name = weaponNode.Attributes[ "name" ].Value;
		mBlueprints.Add( blueprint.Name, blueprint );

		Debug.Log( "Weapon " + blueprint.Name + " loaded" );
	}

}
