using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour {

	public class AttackStats
	{
		public int power = 10;
		public float speed = 1;	//In seconds between swings
		public float range = 1;
	};
	
	
	private AttackStats mBaseAttack = new AttackStats();
	private int mKillCount = 0;
	private int mHealth = 100;

	private float mNextValidAttackTime = 0.0f;

	public AttackStats BaseAttack
	{
		get
		{
			return mBaseAttack;
		}
		set
		{
			mBaseAttack = value;
		}
	}

	public int KillCount
	{
		get
		{
			return mKillCount;
		}
	}

	public int Health
	{
		get
		{
			return mHealth;
		}
	}

	public void AttackTarget( Combatant combatant )
	{
		if( Time.time >= mNextValidAttackTime )
		{
			Vector3 deltaToTarget = combatant.transform.position - transform.position;
			if( deltaToTarget.sqrMagnitude <= mBaseAttack.range * mBaseAttack.range )
			{
				combatant.TakeDamage( this, mBaseAttack.power );
				if( combatant.mHealth <= 0 )
					++mKillCount;
			}
			mNextValidAttackTime = Time.time + mBaseAttack.speed;
		}
	}

	public void TakeDamage( Combatant damageDealer, int damage )
	{
		mHealth -= damage;
		Debug.Log( "Damage taken: " + damage );
	}

	public void AddToKillCount( int count )
	{
		mKillCount += count;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
