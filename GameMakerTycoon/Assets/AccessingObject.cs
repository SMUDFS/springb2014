using UnityEngine;
using System.Collections;

public class AccessingObject : MonoBehaviour {

	//
	//Public
	//
	public GameObject mGoalObject;
	public GameObject mLookAtObject;

	public float mLerpSpeedSeconds;

	//
	//Private
	//
	private bool mIsGoingToObject = false;
	private bool mIsGoingToPlayer = false;
	private bool mIsAccessingObject = false;

	//Lerp Info
	private Vector3 mCameraInitialPos;
	private Quaternion mCameraInitialRotation;

	private Vector3 mGoalPosition;
	private Quaternion mGoalRotation;

	private float mMoveStartTime;

	// Use this for initialization
	void Start () 
	{
		if (!mGoalObject)
			Debug.LogError ("MISSING GOAL POSITION");
		else
			mGoalPosition = mGoalObject.transform.position;

		if (!mLookAtObject)
			Debug.LogError ("MISSING LOOK AT POSITION");
		else
		{
			Transform temp = Instantiate(mGoalObject.transform) as Transform;
			temp.position = mGoalPosition;
			temp.LookAt(mLookAtObject.transform.position); //Calculates goal rotation
			mGoalRotation = temp.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (mIsGoingToObject) 
		{
			if(UpdateMoveCameraToObject())
			{
				mIsGoingToObject = false;
				mIsAccessingObject = true;
			}
		}

		if (mIsAccessingObject) 
		{
			//do stuff
		}

		if (mIsGoingToPlayer) 
		{
			if(UpdateMoveCameraToPlayer())
			{
				mIsGoingToPlayer = false;
			}
		}
	}

	void StartAccessingObject()
	{
		if (!mIsGoingToObject && !mIsAccessingObject && !mIsGoingToPlayer) 
		{
			mCameraInitialPos = Camera.main.transform.position;
			mCameraInitialRotation = Camera.main.transform.rotation;

			mMoveStartTime = Time.realtimeSinceStartup;

			mIsGoingToObject = true;
		} 
		else
			Debug.LogError ("Trevin messed up");
	}

	//Returns true when the camera has arived at the table
	bool UpdateMoveCameraToObject()
	{
		return LerpToNewTransform(
			ref mCameraInitialPos, ref mCameraInitialRotation, 
			ref mGoalPosition, ref mGoalRotation);
	}

	void StopAccessingObject()
	{
		if (mIsAccessingObject) 
		{
			mIsGoingToObject = false;
			mIsGoingToPlayer = true;
			mIsAccessingObject = false;

			mMoveStartTime = Time.realtimeSinceStartup;
		} 
		else
			Debug.LogError ("Trevin messed up");
	}

	//Returns true when the camera has returned to the player
	bool UpdateMoveCameraToPlayer()
	{
		return LerpToNewTransform(
			ref mGoalPosition, ref mGoalRotation, 
			ref mCameraInitialPos, ref mCameraInitialRotation);
	}

	//REturns true when complete
	private bool LerpToNewTransform(
		ref Vector3 initialPos, ref Quaternion initialRot, 
		ref Vector3 goalPos, ref Quaternion goalRot)
	{
		float ALPHA = (Time.realtimeSinceStartup - mMoveStartTime) / mLerpSpeedSeconds;
		
		Vector3 NewPosition = Vector3.Lerp(initialPos, goalPos, ALPHA);
		Quaternion NewRotation = Quaternion.Lerp(initialRot, goalRot, ALPHA);
		
		Camera.main.transform.position = NewPosition;
		Camera.main.transform.rotation = NewRotation;
		
		return ALPHA >= 1.0;
	}
}
