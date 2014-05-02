using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddMonitorUI : MonoBehaviour {

	public GameObject MonitorUI;
	private List<GameObject> mBoyMonitors = new List<GameObject>();
	private List<GameObject> mGirlMonitors = new List<GameObject>();

	public void AddTribute( Tribute t )
	{
		// Find an unused monitor with the correct gender
		GameObject monitor = null;
		List<GameObject> monitorList = null;
		int size = 0;

		if ( t.TheGender == Tribute.Gender.MALE )
			monitorList = mBoyMonitors;
		else
			monitorList = mGirlMonitors;

		size = monitorList.Count;

		if ( size == 0 )
		{
			Debug.LogWarning("No more monitors to add tribute info to!");
			return;
		}

		monitor = monitorList[size-1];
		monitorList.RemoveAt(size-1);

		// Assign this monitor to the tribute
		monitor.GetComponentInChildren<UI_MonitorText>().SetTribute( t );
		Debug.Log("added tribute to monitor ui");
	}

	// Use this for initialization
	void Start ()
	{	
		foreach(GameObject monitor in GameObject.FindGameObjectsWithTag("Monitor"))
		{
			GameObject ui = Instantiate(MonitorUI) as GameObject;
			ui.transform.parent = monitor.transform;

			ui.transform.rotation = monitor.transform.rotation;
			ui.transform.Rotate(new Vector3(0, -180, 0));
			ui.transform.position = monitor.transform.position;

			string[] tok = monitor.name.Split(new char[]{'_'});
			if ( tok[1] == "Boy" )
			{
				Debug.Log(monitor.name + " It's a boy!");
				mBoyMonitors.Add(monitor);
			}
			else
			{
				mGirlMonitors.Add(monitor);
				//Debug.Log(monitor.name + " It's a girl!");
			}

			//Debug.Log("Spawn MonitorUI");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
