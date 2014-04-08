using UnityEngine;
using System.Collections;

public class RotateToMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera playerCam = Camera.main;

		transform.LookAt(playerCam.transform.position);
	}
}
