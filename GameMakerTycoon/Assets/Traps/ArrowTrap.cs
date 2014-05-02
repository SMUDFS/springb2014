using UnityEngine;
using System.Collections;

public class ArrowTrap : Trap {

	public GameObject mArrowPrefab;

	public int mNumberOfArrows = 25;

	public float mDamagePerArrow = 1.0f;
	public float mMaxArrowSpeed = 35.0f;
	public float mMinArrowSpeed = 15.0f;
	public float mAccuracy = 0.25f;
	public float mCooldownSeconds = 10.0f;

	private bool mCanFire = true;
	private float mCooldownTime = -1.0f;

	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();

		if(UnityEngine.Time.time - mCooldownTime >= mCooldownSeconds)
			mCanFire = true;
	}
	
	public override void Trigger()
	{
		Debug.Log("ADSFAD");
		if(mCanFire)
		{
			--mTriggerCount;
			
			ActivateAllParticleSystems();

			mCanFire = false;

			FireArrows();
		}

		//do something
	}
	
	public override bool ShouldDie()
	{
		return mTriggerCount <= 0;
	}

	private void FireArrows()
	{
		for(int i = 0; i < mNumberOfArrows; ++i)
		{
			GameObject arrow = Instantiate(mArrowPrefab) as GameObject;
			Projectile newArrow = arrow.GetComponent<Projectile>();
			newArrow.DamageToDeal = (int)mDamagePerArrow;
			newArrow.Lifetime = 2.0f;

			Vector3 velocity = transform.forward;
			velocity = Quaternion.Euler(
				new Vector3(
					Random.Range(-1.0f + mAccuracy, 1.0f - mAccuracy) * 45.0f, 
					Random.Range(-1.0f + mAccuracy, 1.0f - mAccuracy) * 45.0f, 
					Random.Range(-1.0f + mAccuracy, 1.0f - mAccuracy) * 45.0f)) * velocity;

			velocity *= Random.Range(mMinArrowSpeed, mMaxArrowSpeed);

			arrow.rigidbody.velocity = velocity;

			Vector3 perp = transform.forward;
			float t = perp.x;
			perp.x = perp.z;
			perp.z = -t;

			arrow.transform.position = 
				transform.position
				+ transform.forward * 1.5f
				+ new Vector3(0.0f, Random.Range(0.125f, 0.375f), 0.0f)
				+ perp * Random.Range(-0.5f, 0.5f);


	
			Debug.Log("ADFADSF");
		}
	}
}
