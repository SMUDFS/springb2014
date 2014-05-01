using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Trap : MonoBehaviour {

	public int mTriggerCount = 5;

	private List<ParticleSystem> mTrapParticleSystems = new List<ParticleSystem>();

	// Use this for initialization
	public void Start () 
	{
		GetAllParticlesSystems(gameObject.transform);
		DeactivateAllParticleSystems();
		Debug.Log(mTrapParticleSystems.Count);
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if(ShouldDie())
			Destroy(gameObject);
	}

	public abstract void Trigger();
		
	public abstract bool ShouldDie();

	public void ActivateAllParticleSystems()
	{
		foreach(ParticleSystem ps in mTrapParticleSystems)
			ps.Play();
	}

	public void DeactivateAllParticleSystems()
	{
		foreach(ParticleSystem ps in mTrapParticleSystems)
			ps.Stop();
	}

	private void GetAllParticlesSystems(Transform curTransform)
	{
		GameObject curObj = curTransform.gameObject;
		if(curObj != null)
		{
			ParticleSystem[] components = curObj.GetComponents<ParticleSystem>();
			if(components != null)
			{
				mTrapParticleSystems.AddRange(components);
				for(int i = 0; i < curTransform.childCount; ++i)
					GetAllParticlesSystems(curTransform.GetChild(i));
			}
		}
	}
}
