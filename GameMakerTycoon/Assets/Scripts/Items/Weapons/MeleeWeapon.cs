using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {

	// Use this for initialization
	void Start()
	{}

	protected override bool UseItemSubClass( Tribute targetTribute )
	{
		bool success = false;
		Debug.Log( "Trying to use melee weapon" );
		if( Time.time >= mNextValidAttackTime )
		{
			Vector3 deltaToTarget = targetTribute.gameObject.transform.position - mItemOwner.mItemUseAnchorPoint.position;
			
			Combatant.AttackStats attackStats = this.AttackStats;

			if( deltaToTarget.sqrMagnitude <= attackStats.range * attackStats.range )
			{
				Combatant combatant = targetTribute.GetComponent<Combatant>();
				combatant.TakeDamage( mItemOwner.GetComponent<Combatant>(), attackStats.power );
				if( combatant.Health <= 0 && targetTribute != mItemOwner )
					mItemOwner.GetComponent<Combatant>().AddToKillCount( 1 );
				success = true;
			}
			mNextValidAttackTime = Time.time + attackStats.speed;
			Debug.Log( "Weapon Used" );
		}
		
		return success;
	}
}
