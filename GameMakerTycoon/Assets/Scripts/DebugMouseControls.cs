using UnityEngine;
using System.Collections;

public class DebugMouseControls : MonoBehaviour {

	Tribute mTribute;
	Mover mTributeMover;

	// Use this for initialization
	void Start () {
		mTribute = FindObjectOfType<Tribute>();
		if( mTribute != null )
		{
			mTributeMover = mTribute.GetComponent<Mover>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(1) )
		{

			RaycastHit hitInfo = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if( Physics.Raycast( ray, out hitInfo, 10000 ) )
			{
				Tribute otherTribute = hitInfo.collider.gameObject.GetComponent<Tribute>();
				if( otherTribute != null && otherTribute != mTribute )
				{
					mTribute.UseItem( 0, otherTribute );
				}
				else if( mTributeMover != null )
				{
					Vector3 newTarget = hitInfo.point;
					newTarget.y = 1.0f;
					mTributeMover.CurrentTargetLocation = newTarget;
				}
			}
		}
		else if( Input.GetMouseButtonDown(0) )
		{
			RaycastHit hitInfo = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if( Physics.Raycast( ray, out hitInfo, 10000 ) )
			{
				WeaponLoader loader = FindObjectOfType<WeaponLoader>();
				if( loader != null )
				{
					Vector3 newTarget = hitInfo.point;
					newTarget.y = 3.0f;
					loader.SpawnWeapon( "Bow", newTarget );
				}
			}
		}
		else if( Input.GetKeyDown( "1" ) )
		{
			mTribute.DropItem( 0 );
		}
	}
}
