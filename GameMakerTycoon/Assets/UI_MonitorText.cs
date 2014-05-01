using UnityEngine;
using System.Collections;

public class UI_MonitorText : MonoBehaviour {

	private UILabel mDistrict;
	private UILabel mName;
	private UILabel mRank;
	private UILabel mStatus;

	private Tribute mTribute = null;

	public void SetTribute( Tribute t )
	{
		mTribute = t;
		UpdateStats();
	}

	public void OnTributeDeath()
	{
		mStatus.text = "[ff0000]DEAD";
	}

	private void UpdateStats()
	{
		if ( mTribute == null )
		{
			mDistrict.text = "";
			mName.text = "";
			mRank.text = "";
			mStatus.text = "";
		}
		else
		{
			mDistrict.text = "District " + mTribute.District.ToString();
			mName.text = mTribute.Name;
			mRank.text = "0";
			mStatus.text = "[00ff00]ALIVE";
		}
	}

	// Use this for initialization
	void Start ()
	{
		mDistrict = transform.FindChild("DistrictLabel").gameObject.GetComponent<UILabel>();
		mName     = transform.FindChild("NameLabel").gameObject.GetComponent<UILabel>();
		mRank     = transform.FindChild("RankLabel").gameObject.GetComponent<UILabel>();
		mStatus   = transform.FindChild("StatusLabel").gameObject.GetComponent<UILabel>();

		/*mTribute = new Tribute();
		mTribute.Name = "Bob";
		mTribute.District = 4;*/
		UpdateStats();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
