
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class TributeLoader : MonoBehaviour {

	public TextAsset mTributeXML;
	public TextAsset mNamePoolXML;

	
	private List<TributeBlueprint> mBlueprints = new List<TributeBlueprint>();

	private RandStringPool mMaleNamePool = new RandStringPool();
	private RandStringPool mFemaleNamePool = new RandStringPool();

	private const int NUM_DISTRICTS = 12;

	void Start()
	{
		for( int i = 0; i < NUM_DISTRICTS; ++i )
		{
			mBlueprints.Add( null );
		}
		LoadTributeBlueprints();
		LoadTributeNames();
	}


	public GameObject SpawnTribute( int district, Tribute.Gender gender )
	{
		GameObject tribute = null;
		if( district >= 1 && district <= NUM_DISTRICTS )
		{
			TributeBlueprint blueprint = mBlueprints[ district - 1 ];
			if( blueprint != null )
			{
				tribute = blueprint.SpawnTribute( district, gender );
				if( tribute != null )
				{
					if( gender == Tribute.Gender.MALE )
					{
						tribute.GetComponent<Tribute>().Name = mMaleNamePool.GetRandItem();
					}
					else
					{
						tribute.GetComponent<Tribute>().Name = mFemaleNamePool.GetRandItem();
					}
					tribute.renderer.material.color = new Color( Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f) );
				}
			}
			else
			{
				Debug.Log( "Missing district tribute blueprint for district: " + district );
			}
		}
		return tribute;
	}


	private void LoadTributeBlueprints()
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml( mTributeXML.text );
				
		XmlNodeList tributeNodes = doc.DocumentElement.SelectNodes( "/Tributes/Tribute" );

		foreach( XmlNode tributeNode in tributeNodes )
		{
			TributeBlueprint tributeBlueprint = new TributeBlueprint();
			List<int> districts = XMLUtils.GetAttributeAsListOfInt( tributeNode, "district" );

			TributeBlueprint.AttributeStats aStats = new TributeBlueprint.AttributeStats();
			aStats.intelligence = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Intelligence" ) );
			aStats.aggressiveness = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Aggressiveness" ) );
			aStats.resourcefulness = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Resourcefulness" ) );
			aStats.cooperativeness = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Cooperativeness" ) );
			aStats.courage = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Courage" ) );
			aStats.charisma = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "AttributeStats/Charisma" ) );

			TributeBlueprint.Movement mStats = new TributeBlueprint.Movement();
			mStats.visionRange = XMLUtils.GetXMLPCDataAsRandRangeFloat( tributeNode.SelectSingleNode( "Movement/VisionRange" ) );
			mStats.movementSpeed = XMLUtils.GetXMLPCDataAsRandRangeFloat( tributeNode.SelectSingleNode( "Movement/MovementSpeed" ) );
			mStats.turnSpeed = XMLUtils.GetXMLPCDataAsRandRangeFloat( tributeNode.SelectSingleNode( "Movement/TurnSpeed" ) );

			TributeBlueprint.CombatStats cStats = new TributeBlueprint.CombatStats();
			cStats.power = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "CombatStats/Power" ) );
			cStats.speed = XMLUtils.GetXMLPCDataAsRandRangeFloat( tributeNode.SelectSingleNode( "CombatStats/Speed" ) );
			cStats.range = XMLUtils.GetXMLPCDataAsRandRangeFloat( tributeNode.SelectSingleNode( "CombatStats/Range" ) );
			cStats.maxHealth = XMLUtils.GetXMLPCDataAsRandRangeInt( tributeNode.SelectSingleNode( "CombatStats/MaxHealth" ) );

			tributeBlueprint.AttribStats = aStats;
			tributeBlueprint.MovementStats = mStats;
			tributeBlueprint.CombStats = cStats;

			foreach( int d in districts )
			{
				if( d >= 1 && d <= mBlueprints.Count )
				{
					mBlueprints[d - 1] = tributeBlueprint;
				}
			}
		}

	}

	private void LoadTributeNames()
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml( mNamePoolXML.text );

		XmlNodeList maleNameList = doc.DocumentElement.SelectNodes( "/TributeNames/Male/Name" );

		foreach( XmlNode maleName in maleNameList )
		{
			mMaleNamePool.AddItem( maleName.InnerText );
		}

		XmlNodeList femaleNameList = doc.DocumentElement.SelectNodes( "/TributeNames/Female/Name" );
		
		foreach( XmlNode femaleName in femaleNameList )
		{
			mFemaleNamePool.AddItem( femaleName.InnerText );
		}
	}

}
