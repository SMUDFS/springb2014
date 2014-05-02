using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOEDamage : Trap 
{
	public float mRadius = 10.0f;

	public float mPSTime = 1.5f;
	public float mDamage = 10.0f;

	private float mStartTime = -1.0f;

	private List<Combatant> mEffectedCompatants = new List<Combatant>();

	private SphereCollider mTriggerCollider;

	// Use this for initialization
	new void Start () 
	{
		Debug.Log("ADFFFFFFFFFF");
		mTriggerCollider = gameObject.AddComponent<SphereCollider>();
		mTriggerCollider.radius = mRadius;
		mTriggerCollider.isTrigger = true;

		base.Start();
	}
	
	// Update is called once per frame
	new void Update () 
	{
		base.Update();
	}

	public override void Trigger()
	{
		--mTriggerCount;

		mStartTime = UnityEngine.Time.time;

		ActivateAllParticleSystems();

		DealDamage(mDamage);

		//do something
	}

	protected void DealDamage(float damage)
	{
		for(int i = 0; i < mEffectedCompatants.Count;)
		{
			Combatant c = mEffectedCompatants[i];
			c.TakeDamage(null, Mathf.CeilToInt(damage));
			             
             if(c.CurrentHealth <= 0.0f)
             	mEffectedCompatants.Remove(c);
             else
             	++i;
        }
	}

	void OnTriggerEnter(Collider c) 
	{
		Combatant tribute = c.GetComponent<Combatant>();
		if(tribute != null)
		{
			if(!mEffectedCompatants.Contains(tribute))
				mEffectedCompatants.Add(tribute);
		}
	}
	
	void OnTriggerExit(Collider c)
	{
		Combatant tribute = c.GetComponent<Combatant>();
		if(tribute != null)
		{
			mEffectedCompatants.Remove(tribute);
		}
	}

	public override bool ShouldDie()
	{
		return mTriggerCount <= 0 && UnityEngine.Time.time - mStartTime >= mPSTime;
	}
}
