using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ItemLoader : MonoBehaviour {


	public TextAsset mItemXML;

	private Dictionary<string, ItemBlueprint> mBlueprints = new Dictionary<string, ItemBlueprint>();



	public GameObject SpawnItem( string name, Vector3 pos )
	{
		ItemBlueprint blueprint = null;
		mBlueprints.TryGetValue( name, out blueprint );
		
		if( blueprint != null )
		{
			return blueprint.SpawnItem( pos );
		}
		else
		{
			return null;
		}
	}


	// Use this for initialization
	void Start () {
		LoadItems();
	}



	private void LoadItems()
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml( mItemXML.text );

		XmlNodeList items = doc.DocumentElement.SelectNodes( "Items/Item" );

		foreach( XmlNode itemNode in items )
		{
			ItemBlueprint newItemBP = new ItemBlueprint();
			ItemBlueprint.ItemEffectsBP effect = new ItemBlueprint.ItemEffectsBP();

			newItemBP.Name = itemNode.Attributes[ "name" ].Value;

			string numUsesStr = itemNode.Attributes[ "numUses" ].Value;
			int numUses = 0;
			int.TryParse( numUsesStr, out numUses );
			newItemBP.NumUses = numUses;

			effect.modToHealth = XMLUtils.GetAttributeAsRandRangeInt( itemNode, "health" );
			effect.modToHealth = XMLUtils.GetAttributeAsRandRangeInt( itemNode, "hunger" );
			effect.modToHealth = XMLUtils.GetAttributeAsRandRangeInt( itemNode, "thirst" );

			newItemBP.PrefabName = itemNode.Attributes[ "prefab" ].Value;

			if( newItemBP.Name.Length > 0 )
				mBlueprints.Add( newItemBP.Name, newItemBP );

		}
	}



}
