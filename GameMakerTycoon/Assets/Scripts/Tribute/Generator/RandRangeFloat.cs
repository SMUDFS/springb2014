using UnityEngine;
using System.Collections;

public class RandRangeFloat {

	private float mMinValue = 0.0f;
	private float mMaxValue = 0.0f;

	public RandRangeFloat( float min, float max )
	{
		mMinValue = min;
		mMaxValue = max;
	}

	public float GetRandNum()
	{
		return Random.Range( mMinValue, mMaxValue );
	}
}
