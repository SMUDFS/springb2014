using UnityEngine;
using System.Collections;

public class Weapon : Item {

	private Combatant.AttackStats mAttackStats = new Combatant.AttackStats();
		
	protected float mNextValidAttackTime = 0.0f;

	public Combatant.AttackStats AttackStats
	{
		get
		{
			return mAttackStats;
		}
		set
		{
			mAttackStats = value;
			mItemEffect.modToHealth = -mAttackStats.power;
		}
	}
}
