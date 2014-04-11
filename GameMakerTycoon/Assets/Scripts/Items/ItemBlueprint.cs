using UnityEngine;
using System.Collections;

public class ItemBlueprint {

	public class ItemEffectsBP
	{
		public RandRangeInt modToHealth;
		public RandRangeInt modToHunger;
		public RandRangeInt modToThirst;
	}

	private string mName;
	private string mPrefabName;
	private int mNumUses;
	private ItemEffectsBP mEffects;

	public string Name
	{
		get { return mName; }
		set { mName = value; }
	}

	public string PrefabName
	{
		get { return mPrefabName; }
		set { mPrefabName = value; }
	}

	public int NumUses
	{
		get { return mNumUses; }
		set { mNumUses = value; }
	}

	public GameObject SpawnItem( Vector3 pos )
	{
		Debug.Log( "attempting to spawn item: " + mName );
		GameObject item = GameObject.Instantiate( Resources.Load( "Items/" + this.PrefabName, typeof(GameObject) ) ) as GameObject;
		item.transform.position = pos;
		Item itemComp = item.AddComponent<Item>();
		itemComp.NumberOfUses = this.mNumUses;
		itemComp.Name = this.Name;

		Item.ItemEffect effect = new Item.ItemEffect();
		effect.modToHealth = mEffects.modToHealth.GetRandNum();
		effect.modToHungerLevel = mEffects.modToHunger.GetRandNum();
		effect.modToThirstLevel = mEffects.modToThirst.GetRandNum();

		itemComp.Effect = effect;
		return item;
	}

}
