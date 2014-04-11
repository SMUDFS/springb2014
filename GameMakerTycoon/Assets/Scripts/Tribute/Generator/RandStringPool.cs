using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandStringPool {

	private List<string> mStringPool = new List<string>();

	public void AddItem( string item )
	{
		mStringPool.Add( item );
	}

	public string GetRandItem()
	{
		string item = "No More Strings In Pool";
		if( mStringPool.Count > 0 )
		{
			int idx = Random.Range( 0, mStringPool.Count );
			item = mStringPool[idx];
			mStringPool.Remove( item );
		}

		return item;
	}
}
