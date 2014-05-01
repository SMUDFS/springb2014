using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOEDOTDamage : AOEDamage 
{
	public float mOnTimeSeconds = 5.0f;

	private bool mIsOn = false;
	private float mOnStartTime = -1.0f;

	// Update is called once per frame
	new void Update () 
	{
		if(mIsOn)
		{
			DealDamage(mDamage * UnityEngine.Time.deltaTime);

			if(UnityEngine.Time.time - mOnStartTime > mOnTimeSeconds)
			{
				DeactivateAllParticleSystems();
				mIsOn = false;
			}

			transform.Rotate(new Vector3(0.0f, 10.0f, 0.0f));
		}

		base.Update();
	}

	public override void Trigger()
	{
		mTriggerCount -= 1;

		if(!mIsOn)
		{
			mIsOn = true;
			mOnStartTime = UnityEngine.Time.time;
			ActivateAllParticleSystems();
		}
		//do something
	}

	public override bool ShouldDie()
	{
		return mTriggerCount <= 0 && !mIsOn;
	}
}
