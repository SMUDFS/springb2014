using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class BaseAttack : RAINAction
{
    public BaseAttack()
    {
        actionName = "BaseAttack";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {

		Tribute tribComp = ai.Body.GetComponent<Tribute>();
		if( tribComp != null )
		{
			GameObject targetTribute = ai.WorkingMemory.GetItem<GameObject>( "varTribute" );
			if( targetTribute != null )
			{
				tribComp.GetComponent<Combatant>().AttackTarget( targetTribute.GetComponent<Combatant>() );
			}
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}