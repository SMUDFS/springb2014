using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public bool mShouldDrawDebugSphere = false;
	public GameObject mSpherePrefab;
	public GameObject mDebugSphere;

	private float mMovementSpeed = 1.0f;
	private float mRotationSpeed = 1.0f;
	public Vector3 mCurrentTargetLocation;
	private Vector3 mCurrentDirectionFacing;


	public float MovementSpeed
	{
		get
		{
			return mMovementSpeed;
		}
		set
		{
			mMovementSpeed = value;
		}
	}

	public float RotationSpeed
	{
		get
		{
			return mRotationSpeed;
		}
		set
		{
			mRotationSpeed = value;
		}
	}

	public Vector3 CurrentTargetLocation
	{
		get
		{
			return mCurrentTargetLocation;
		}
		set
		{
			mCurrentTargetLocation = value;
			if( mDebugSphere != null )
			{
				mDebugSphere.transform.position = mCurrentTargetLocation;
			}
		}
	}

	public Vector3 CurrentDirectionFacing
	{
		get
		{
			return mCurrentDirectionFacing;
		}
		set
		{
			mCurrentDirectionFacing = value;
		}
	}


	// Use this for initialization
	void Start () {
		if( mShouldDrawDebugSphere && mSpherePrefab != null )
		{
			mDebugSphere = Instantiate( mSpherePrefab ) as GameObject;
			mDebugSphere.transform.position = mCurrentTargetLocation;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		Vector3 deltaToTarget = mCurrentTargetLocation - transform.position;
		if( deltaToTarget.sqrMagnitude > 0.1f )
		{
			gameObject.transform.position += transform.forward * mMovementSpeed * Time.fixedDeltaTime;

			deltaToTarget.y = 0;
			if( deltaToTarget.sqrMagnitude != 0 )
			{
				Vector3 newDir = Vector3.RotateTowards( transform.forward, deltaToTarget.normalized, Time.deltaTime * mRotationSpeed, 0.0f );
				gameObject.transform.rotation = Quaternion.LookRotation( newDir );
			}
		}
	}
}
