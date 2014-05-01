using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class ShouldRunOrFight : RAINAction
{
    public ShouldRunOrFight()
    {
        actionName = "ShouldRunOrFight";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		//setting bool shouldRun
		bool shouldRun = false;
		Tribute tribComp = ai.Body.GetComponent<Tribute>();
		Combatant comb = ai.Body.GetComponent<Combatant>();
		if( tribComp != null && tribComp.AttribStats != null )
		{
			float percentage = (float)tribComp.AttribStats.courage / (float)tribComp.AttribStats.maxAttribScore;
			int healthThresholdLevel = (int)( (float)comb.MaxHealth * ( 1 - percentage ) );

			shouldRun = comb.CurrentHealth < healthThresholdLevel && Random.Range( 0.0f, 1.0f ) > percentage;
			//Debug.Log( "Theshold: " + healthThresholdLevel );
		}
		ai.WorkingMemory.SetItem<bool>( "shouldRun", shouldRun );
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}