using UnityEngine;
using System.Collections;

public class TributeBlueprint {

	public class AttributeStats
	{
		public RandRangeInt intelligence;
		public RandRangeInt aggressiveness;
		public RandRangeInt resourcefulness;
		public RandRangeInt cooperativeness;
		public RandRangeInt courage;
		public RandRangeInt charisma;
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
				Mover moverComp = tribute.GetComponent<Mover>();
				Combatant combComp = tribute.GetComponent<Combatant>();

				Tribute.AttributeStats aStats = new Tribute.AttributeStats();
				aStats.intelligence = mAttributeStats.intelligence.GetRandNum();
				aStats.aggressiveness = mAttributeStats.aggressiveness.GetRandNum();
				aStats.resourcefulness = mAttributeStats.resourcefulness.GetRandNum();
				aStats.cooperativeness = mAttributeStats.cooperativeness.GetRandNum();
				aStats.courage = mAttributeStats.courage.GetRandNum();
				aStats.charisma = mAttributeStats.charisma.GetRandNum();

				tribComp.AttribStats = aStats;
				tribComp.District = district;
				tribComp.TheGender = gender;

				moverComp.MovementSpeed = mMovement.movementSpeed.GetRandNum();
				moverComp.TurnSpeed = mMovement.turnSpeed.GetRandNum();
				moverComp.VisionRange = mMovement.visionRange.GetRandNum();

				Combatant.AttackStats cStats = new Combatant.AttackStats();
				cStats.power = mCombatStats.power.GetRandNum();
				cStats.range = mCombatStats.range.GetRandNum();
				cStats.speed = mCombatStats.speed.GetRandNum();
				combComp.BaseAttack = cStats;
				combComp.MaxHealth = mCombatStats.maxHealth.GetRandNum();
			}

		}

		return tribute;
	}

}
