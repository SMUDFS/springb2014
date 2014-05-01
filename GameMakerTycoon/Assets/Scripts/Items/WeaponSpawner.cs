
using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour {
	
	public string mItemName;
	
	public bool mShouldSpawn = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if( mShouldSpawn )
		{
			WeaponLoader loader = FindObjectOfType<WeaponLoader>();
			if( loader != null )
			{
				GameObject weapon = loader.SpawnWeapon( mItemName, transform.position );
				if( weapon != null )
				{
					Debug.Log( "Weapon Spawned: Name: " + weapon.GetComponent<Weapon>().Name );
				}
				Debug.Log( "Tried to spawn weapon" );
			}
			mShouldSpawn = false;
		}
	}
}
