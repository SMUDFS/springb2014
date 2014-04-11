using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	private Tribute mTributeThatLaunchedMe;
	private int mDamageToDeal = 0;

	private float mLifetime = 5.0f;
	private float mCurrentLifetime = 0.0f;

	public float Lifetime
	{
		get
		{
			return mLifetime;
		}
		set
		{
			mLifetime = value;
		}
	}

	public Tribute TributeThatLaunchedMe
	{
		get
		{
			return mTributeThatLaunchedMe;
		}
		set
		{
			mTributeThatLaunchedMe = value;
		}
	}

	public int DamageToDeal
	{
		get
		{
			return mDamageToDeal;
		}
		set
		{
			mDamageToDeal = value;
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if( mLifetime > 0 )
		{
			if( mCurrentLifetime > mLifetime )
				Destroy( gameObject );
			mCurrentLifetime += Time.deltaTime;
		}

		gameObject.transform.rotation = Quaternion.LookRotation( gameObject.rigidbody.velocity.normalized );
	}

	void OnCollisionEnter( Collision collision )
	{
		Tribute target = collision.gameObject.GetComponent<Tribute>();
		if( target != null )
		{
			DealDamageToTarget( target );
		}
		
		Destroy( gameObject );
	}

	protected void DealDamageToTarget( Tribute hitTribute )
	{
		if( hitTribute != mTributeThatLaunchedMe )
		{
			Combatant combatant = hitTribute.GetComponent<Combatant>();
			combatant.TakeDamage( mTributeThatLaunchedMe.GetComponent<Combatant>(), mDamageToDeal );
			if( combatant.MaxHealth <= 0 && hitTribute != mTributeThatLaunchedMe )
				mTributeThatLaunchedMe.GetComponent<Combatant>().AddToKillCount( 1 );
		}
	}
}
