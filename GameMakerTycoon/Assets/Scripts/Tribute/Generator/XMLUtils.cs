using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class XMLUtils {


	public static RandRangeFloat GetXMLPCDataAsRandRangeFloat( XmlNode node )
	{
		RandRangeFloat ranFloat = null;
		string innerText = node.InnerText;
		if( innerText.Length > 0 )
		{
			ranFloat = ParseStringAsFloatRange( innerText, node );
		}

		return ranFloat;
	}

	public static RandRangeInt GetXMLPCDataAsRandRangeInt( XmlNode node )
	{
		RandRangeInt ranInt = null;
		string innerText = node.InnerText;
		if( innerText.Length > 0 )
		{
			ranInt = ParseStringAsIntRange( innerText, node );
		}
		
		return ranInt;
	}

	public static RandRangeFloat GetAttributeAsRandRangeFloat( XmlNode node, string attributeName )
	{
		RandRangeFloat ranFloat = null;
		string attributeText = node.Attributes[ attributeName ].Value;
		if( attributeText.Length > 0 )
		{
			ranFloat = ParseStringAsFloatRange( attributeText, node );
		}
		return ranFloat;
	}

	public static RandRangeInt GetAttributeAsRandRangeInt( XmlNode node, string attributeName )
	{
		RandRangeInt ranInt = null;
		string attributeText = node.Attributes[ attributeName ].Value;
		if( attributeText.Length > 0 )
		{
			ranInt = ParseStringAsIntRange( attributeText, node );
		}
		return ranInt;
	}



	public static List<int> GetAttributeAsListOfInt( XmlNode node, string attributeName )
	{
		string attributeStr = node.Attributes[ attributeName ].Value;
		attributeStr.Trim();
		string[] listElements = attributeStr.Split( ',' );

		List<int> elementList = new List<int>();

		foreach( string s in listElements )
		{
			int value = 0;
			s.Trim();
			if( int.TryParse( s, out value ) )
			{
				elementList.Add( value );
			}
		}

		return elementList;
	}

	private static RandRangeFloat ParseStringAsFloatRange( string range, XmlNode node )
	{
		RandRangeFloat ranFloat = null;

		range.Trim();
		string[] rangeValues = range.Split( '~' );
		if( rangeValues.Length == 2 )
		{
			float min = 0;
			float max = 1;
			if( !float.TryParse( rangeValues[0], out min ) )
			{
				Debug.Log( "Invalid min value for range on node: " + node.Name );
			}
			if( !float.TryParse( rangeValues[1], out max ) )
			{
				Debug.Log( "Invalid max value for range on node: " + node.Name );
			}
			ranFloat = new RandRangeFloat( min, max );
		}
		else if( range.Length == 1 )
		{
			float value = 0;
			if( !float.TryParse( rangeValues[0], out value ) )
			{
				Debug.Log( "Invalid value specified for node: " + node.Name );
			}
			ranFloat = new RandRangeFloat( value, value );
		}
		else
		{
			Debug.Log( "Invalid float range specified for node: " + node.Name );
		}

		return ranFloat;
	}

	private static RandRangeInt ParseStringAsIntRange( string range, XmlNode node )
	{
		RandRangeInt ranInt = null;

		range.Trim();
		string[] rangeValues = range.Split( '~' );
		if( rangeValues.Length == 2 )
		{
			int min = 0;
			int max = 1;
			if( !int.TryParse( rangeValues[0], out min ) )
			{
				Debug.Log( "Invalid min value for range on node: " + node.Name );
			}
			if( !int.TryParse( rangeValues[1], out max ) )
			{
				Debug.Log( "Invalid max value for range on node: " + node.Name );
			}
			ranInt = new RandRangeInt( min, max );
		}
		else if( range.Length == 1 )
		{
			int value = 0;
			if( !int.TryParse( rangeValues[0], out value ) )
			{
				Debug.Log( "Invalid value specified for node: " + node.Name );
			}
			ranInt = new RandRangeInt( value, value );
		}
		else
		{
			Debug.Log( "Invalid float range specified for node: " + node.Name );
		}

		return ranInt;
	}
}
