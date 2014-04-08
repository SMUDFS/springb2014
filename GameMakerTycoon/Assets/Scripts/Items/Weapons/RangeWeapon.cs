using UnityEngine;
using System.Collections;

public class RangeWeapon : Weapon {

	public GameObject mProjectilePrefab;

	public float mProjectileSpeed;


	// Use this for initialization
	void Start()
	{
		Combatant.AttackStats attackStats = this.AttackStats;
		attackStats.power = 100;
		attackStats.range = 100;
		this.AttackStats = attackStats;
	}
	
	protected override bool UseItemSubClass( Tribute targetTribute )
	{
		bool success = false;
		Debug.Log( "Trying to use ranged weapon" );
		if( Time.time >= mNextValidAttackTime )
		{
			Vector3 deltaToTarget = targetTribute.gameObject.transform.position - mItemOwner.mItemUseAnchorPoint.position;
			
			Combatant.AttackStats attackStats = this.AttackStats;
			
			if( deltaToTarget.sqrMagnitude <= attackStats.range * attackStats.range )
			{
				Vector3 dirToTarget = deltaToTarget.normalized;
				LaunchProjectile( dirToTarget * mProjectileSpeed );
				success = true;
			}
			mNextValidAttackTime = Time.time + attackStats.speed;
			Debug.Log( "Weapon Used" );
		}
		
		return success;
	}

	protected void LaunchProjectile( Vector3 velocity )
	{
		GameObject projectile = GameObject.Instantiate( mProjectilePrefab, mItemOwner.mItemUseAnchorPoint.position, mItemOwner.mItemUseAnchorPoint.rotation ) as GameObject;
		if( projectile != null )
		{
			Debug.Log( "Firing Proj" );
			Projectile projComp = projectile.GetComponent<Projectile>();
			if( projComp != null )
			{
				projComp.TributeThatLaunchedMe = mItemOwner;
				projComp.DamageToDeal = AttackStats.power;
				projComp.transform.rotation = Quaternion.LookRotation( velocity );

				Rigidbody projRigidbody = projectile.rigidbody;
				if( projRigidbody == null )
				{
					projRigidbody = projectile.AddComponent<Rigidbody>();
				}
				projRigidbody.useGravity = true;

				projRigidbody.transform.rotation = Quaternion.LookRotation( velocity );

				projRigidbody.velocity = velocity;
			}
		}
	}
}
