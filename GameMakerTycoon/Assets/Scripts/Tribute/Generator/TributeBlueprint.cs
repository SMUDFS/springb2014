using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Perception;

public class TributeBlueprint {

	public class AttributeStats
	{
		public RandRangeInt intelligence;
		public RandRangeInt aggressiveness;
		public RandRangeInt resourcefulness;
		public RandRangeInt cooperativeness;
		public RandRangeInt courage;
		public RandRangeInt charisma;
		public int maxAttribScore;
	};

	public class Movement
	{
		public RandRangeFloat visionRange;
		public RandRangeFloat movementSpeed;
		public RandRangeFloat turnSpeed;
	};

	public class CombatStats
	{
		public RandRangeInt power;
		public RandRangeFloat speed;
		public RandRangeFloat range;
		public RandRangeInt maxHealth;
	};

	private AttributeStats mAttributeStats;
	private Movement mMovement;
	private CombatStats mCombatStats;

	private int mAttributeTotal;

	public AttributeStats AttribStats
	{
		get
		{
			return mAttributeStats;
		}
		set
		{
			mAttributeStats = value;
		}
	}

	public Movement MovementStats
	{
		get
		{
			return mMovement;
		}
		set
		{
			mMovement = value;
		}
	}

	public CombatStats CombStats
	{
		get
		{
			return mCombatStats;
		}
		set
		{
			mCombatStats = value;
		}
	}


	public GameObject SpawnTribute( int district, Tribute.Gender gender )
	{
		GameObject tribute = GameObject.Instantiate( Resources.Load( "Tribute/Tribute", typeof(GameObject) ) ) as GameObject;

		if( tribute != null )
		{
			Tribute tribComp = tribute.GetComponent<Tribute>();
			if( tribComp != null )
			{
				Combatant combComp = tribute.GetComponent<Combatant>();

				Tribute.AttributeStats aStats = new Tribute.AttributeStats();
				aStats.intelligence = mAttributeStats.intelligence.GetRandNum();
				aStats.aggressiveness = mAttributeStats.aggressiveness.GetRandNum();
				aStats.resourcefulness = mAttributeStats.resourcefulness.GetRandNum();
				aStats.cooperativeness = mAttributeStats.cooperativeness.GetRandNum();
				aStats.courage = mAttributeStats.courage.GetRandNum();
				aStats.charisma = mAttributeStats.charisma.GetRandNum();
				aStats.maxAttribScore = mAttributeStats.maxAttribScore;

				tribComp.AttribStats = aStats;
				tribComp.District = district;
				tribComp.TheGender = gender;

				tribComp.MovementSpeed = mMovement.movementSpeed.GetRandNum();
				tribComp.TurnSpeed = mMovement.turnSpeed.GetRandNum();
				tribComp.VisionRange = mMovement.visionRange.GetRandNum();

				Combatant.AttackStats cStats = new Combatant.AttackStats();
				cStats.power = mCombatStats.power.GetRandNum();
				cStats.range = mCombatStats.range.GetRandNum();
				cStats.speed = mCombatStats.speed.GetRandNum();
				combComp.BaseAttack = cStats;
				combComp.MaxHealth = mCombatStats.maxHealth.GetRandNum();

				AIRig ai = tribute.GetComponentInChildren<AIRig>();

				if( ai != null )
				{
					ai.AI.Motor.DefaultSpeed = tribComp.MovementSpeed;
					ai.AI.Motor.DefaultRotationSpeed = tribComp.TurnSpeed;
					( ai.AI.Senses.GetSensor( "ears" ) as RAIN.Perception.Sensors.AudioSensor ).Range  = tribComp.VisionRange;
					( ai.AI.Senses.GetSensor( "eyes" ) as RAIN.Perception.Sensors.VisualSensor ).Range  = tribComp.VisionRange;
					//ai.AI.Motor.DefaultCloseEnoughDistance = tribute.collider.bounds.size.z;
					ai.AI.WorkingMemory.SetItem<float>( "varCloseEnoughDistance", ai.AI.Motor.DefaultCloseEnoughDistance );
					Debug.Log( "Default close distance: " +  ai.AI.Motor.DefaultCloseEnoughDistance );
				}

			}

		}

		return tribute;
	}

}
