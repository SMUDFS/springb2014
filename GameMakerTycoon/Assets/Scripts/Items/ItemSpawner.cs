using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public string mItemName;
	
	public bool mShouldSpawn = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if( mShouldSpawn )
		{
			ItemLoader loader = FindObjectOfType<ItemLoader>();
			if( loader != null )
			{
				GameObject item = loader.SpawnItem( mItemName, transform.position );
				if( item != null )
				{
					Debug.Log( "Item Spawned: Name: " + item.GetComponent<Item>().Name );
				}
				Debug.Log( "Tried to spawn item" );
			}
			mShouldSpawn = false;
		}
	}
}
