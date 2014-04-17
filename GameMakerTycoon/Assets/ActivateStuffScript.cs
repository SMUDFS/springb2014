using UnityEngine;
using System.Collections;

public delegate void StartGUIEvent(GameObject accessedObject);

public class ActivateStuffScript : MonoBehaviour {

	//Basic Stuff
	//
	public int mRaycastDistance = 50;
	public LayerMask mRaycastLayerMask;

	private bool mInputDown = false;

	//Accessing Something
	//
	public StartGUIEvent mGUIStartFunc = null;

	private bool mIsGoingToObject = false;
	private bool mIsGoingToPlayer = false;
	private bool mIsAccessing = false;

	private GameObject mAccessingObject;

	//Lerp Info
	public float mLerpDurSec;

	private Vector3 mCameraInitialPos;
	private Quaternion mCameraInitialRotation;
	
	private Vector3 mGoalPosition;
	private Quaternion mGoalRotation;
	
	private float mMoveStartTime;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleInput();

		HandleAccessing();
	}

	////////////////
	//Input Handling
	////////////////
	private void HandleInput()
	{
		if (Input.GetButtonDown("Action")) 
		{
			if(!mInputDown)
			{
				if(!mIsAccessing)
				{
					Transform camTrans = Camera.main.transform;

					RaycastHit[] hits;
					hits = Physics.RaycastAll(camTrans.position, camTrans.forward, mRaycastDistance, mRaycastLayerMask);
					HandleRaycast(ref hits);
				}

				mInputDown = true;
			}
		} 
		else
			mInputDown = false;
	}
	
	private void HandleRaycast(ref RaycastHit[] hits)
	{
		float closestDistance = float.MaxValue;
		GameObject closestObject = null;
		for(int i = 0; i < hits.Length; ++i)
		{
			RaycastHit hit = hits[i];
			Collider currentCollider = hit.collider;

			if(currentCollider && hit.distance < closestDistance)
			{
				closestDistance = hit.distance;
				closestObject = currentCollider.gameObject;
			}
		}

		if(closestObject)
			HandleObjectClicked(ref closestObject);
	}

	private void HandleObjectClicked(ref GameObject someObject)
	{
		if (!mIsAccessing) 
		{
			//Find goal position and lookat position sprite components
			Transform posTrans = null;
			Transform lookAtTrans = null;
			foreach(Transform child in someObject.transform)
			{
				if(child.tag.CompareTo("GoToPos") == 0)
					posTrans = child.transform;
				else if(child.tag.CompareTo("LookAtPos") == 0)
					lookAtTrans = child.transform;

				if(posTrans && lookAtTrans)
				{
					mAccessingObject = someObject;
					break;
				}
			}

			//Get and calculate goal position and rotation
			if(posTrans && lookAtTrans)
			{
				Debug.Log("FOUND");
				mGoalPosition = posTrans.position;

				Vector3 tempPos = transform.position;
				Quaternion tempRot = transform.rotation;

				transform.position = mGoalPosition;
				transform.LookAt(lookAtTrans.position); //Calculates goal rotation
				mGoalRotation = transform.rotation;

				transform.position = tempPos;
				transform.rotation = tempRot;

				StartAccessingObject();
			}
		} 
	}

	private void SetMovementInputEnabled(bool enabled)
	{
		//disable movement
		Component movementComponent = this.GetComponent ("CharacterMotor");
		if (movementComponent) {
			movementComponent.SendMessage ("SetEnabled", enabled);
		}
		
		//disable camera y rotation
		Component[] mouseLookComponents;
		mouseLookComponents = GetComponentsInChildren<MouseLook> ();
		foreach (MouseLook mouseLook in mouseLookComponents) {
			mouseLook.isEnabled = enabled;
		}
	}
	
	/////////////////////////
	//Handle Object Accessing
	/////////////////////////
	private void HandleAccessing()
	{
		if (mIsGoingToObject) 
		{
			if(UpdateMoveCameraToObject())
			{
				ArrivedAtObject();
			}
		}

		if (mIsGoingToPlayer) 
		{
			if(UpdateMoveCameraToPlayer())
			{
				StoppedAccessingObject();
			}
		}

		if(mAccessingObject && !mIsGoingToObject)
		{
			HungerGamesMap mapComponent = mAccessingObject.GetComponent<HungerGamesMap>();
			if(mapComponent && !mapComponent.mTableActivated)
			{
				StopAccessingObject();
				mAccessingObject = null;
			}
		}
	}

	//Going towards object
	void StartAccessingObject()
	{
		if (!mIsGoingToObject && !mIsAccessing && !mIsGoingToPlayer) 
		{
			mCameraInitialPos = Camera.main.transform.position;
			mCameraInitialRotation = Camera.main.transform.rotation;
			
			mMoveStartTime = Time.realtimeSinceStartup;

			SetMovementInputEnabled(false);

			mIsGoingToObject = true;
			mIsAccessing = true;
		} 
		else
			Debug.LogError ("Trevin messed up");
	}

	private void ArrivedAtObject()
	{
		mIsGoingToObject = false;
		HungerGamesMap mapComponent = mAccessingObject.GetComponent<HungerGamesMap>();
		if(mapComponent)
		{
			mapComponent.mTableActivated = true;
		}

		if(mGUIStartFunc != null)
			mGUIStartFunc(mAccessingObject);
	}
	
	//Returns true when the camera has arived at the table
	private bool UpdateMoveCameraToObject()
	{
		return LerpToNewTransform(
			ref mCameraInitialPos, ref mCameraInitialRotation, 
			ref mGoalPosition, ref mGoalRotation);
	}

	//Going back to player
	private void StopAccessingObject()
	{
		if (mIsAccessing) 
		{
			mIsGoingToObject = false;
			mIsGoingToPlayer = true;
			
			mMoveStartTime = Time.realtimeSinceStartup;
		} 
		else
			Debug.LogError ("Trevin messed up");
	}

	private void StoppedAccessingObject()
	{
		mIsGoingToPlayer = false;
		mIsAccessing = false;
		
		SetMovementInputEnabled(true);
	}
	
	//Returns true when the camera has returned to the player
	private bool UpdateMoveCameraToPlayer()
	{
		return LerpToNewTransform(
			ref mGoalPosition, ref mGoalRotation, 
			ref mCameraInitialPos, ref mCameraInitialRotation);
	}

	//Returns true when complete
	private bool LerpToNewTransform(
		ref Vector3 initialPos, ref Quaternion initialRot, 
		ref Vector3 goalPos, ref Quaternion goalRot)
	{
		float ALPHA = (Time.realtimeSinceStartup - mMoveStartTime) / mLerpDurSec;
		
		Vector3 NewPosition = Vector3.Lerp(initialPos, goalPos, ALPHA);
		Quaternion NewRotation = Quaternion.Lerp(initialRot, goalRot, ALPHA);
		
		Camera.main.transform.position = NewPosition;
		Camera.main.transform.rotation = NewRotation;
		
		return ALPHA >= 1.0;
	}
}
