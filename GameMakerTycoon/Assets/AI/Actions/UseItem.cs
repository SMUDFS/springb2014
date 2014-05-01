using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class UseItem : RAINAction
{
    public UseItem()
    {
        actionName = "UseItem";
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
			int currentItemIdx = ai.WorkingMemory.GetItem<int>( "currentItemIdxToUse" );
			GameObject targetTribute = ai.WorkingMemory.GetItem<GameObject>( "varTribute" );
			if( currentItemIdx != -1 && targetTribute != null )
			{
				Vector3 deltaToTarget = targetTribute.transform.position - ai.Body.transform.position;  
				float itemRange = tribComp.TheInventory.GetItem( currentItemIdx ).ItemRange;
				if( deltaToTarget.sqrMagnitude <= itemRange * itemRange )
				{
					tribComp.UseItem( currentItemIdx, targetTribute.GetComponent<Tribute>() );
				}
			}
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}