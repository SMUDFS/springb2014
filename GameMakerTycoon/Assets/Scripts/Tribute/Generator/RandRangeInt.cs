using UnityEngine;
using System.Collections;

public class RandRangeInt {

	private int mMinValue = 0;
	private int mMaxValue = 1;

	public RandRangeInt( int min, int max )
	{
		mMinValue = min;
		mMaxValue = max;
	}

	public int GetRandNum()
	{
		return Random.Range( mMinValue, mMaxValue );
	}
}
