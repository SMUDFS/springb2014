using UnityEngine;
using System.Collections;

public class FireballTrap : Trap {

	public float mDamageRadius = 10.0f;
	public float mDamage = 10.0f;
	public float mCooldown = 1.0f;

	private float mStartTime = -1.0f;

	private bool mhasFired = false;

	private ParticleSystem.CollisionEvent[] mCollisionEvents = new ParticleSystem.CollisionEvent[16];

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Trigger()
	{	
		if(!mhasFired && UnityEngine.Time.time - mStartTime >= mCooldown)
		{
			mTriggerCount -= 1;
			ActivateAllParticleSystems();
			mStartTime = UnityEngine.Time.time;
		}
	}
	
	public override bool ShouldDie()
	{
		return mTriggerCount <= 0 && !mhasFired;
	}

	void OnParticleCollision(GameObject other) 
	{
		mhasFired = false;

		ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
		if(ps != null)
		{
			int numEvents = ps.safeCollisionEventSize;
			if(mCollisionEvents.Length < numEvents)
				mCollisionEvents = new ParticleSystem.CollisionEvent[numEvents];

			numEvents = ps.GetCollisionEvents(other, mCollisionEvents);
			for(int i = 0; i < numEvents; ++i)
			{
				Collider[] colliders = Physics.OverlapSphere(mCollisionEvents[i].intersection, mDamageRadius);
				for(int j = 0; j < colliders.Length; ++j)
				{
					if(colliders[j].gameObject != null)
					{
						Combatant combatant = colliders[j].gameObject.GetComponent<Combatant>();
						if(combatant != null)
							combatant.TakeDamage(null, (int)mDamage);
					}
				}
			}
		}
		else
			Debug.LogWarning("No PS on: " + name);
	}
}
