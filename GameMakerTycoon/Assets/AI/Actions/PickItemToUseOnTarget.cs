using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class PickItemToUseOnTarget : RAINAction
{
    public PickItemToUseOnTarget()
    {
        actionName = "PickItemToUseOnTarget";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		Tribute tribComp = ai.Body.GetComponent<Tribute>();
		
		int currentItemIdx = -1;

		if( tribComp != null )
		{
			int count = tribComp.TheInventory.Count;
			Item.ItemEffect effect = new Item.ItemEffect();

			for( int i = 0; i < count; ++i )
			{
				if( effect.modToHealth > tribComp.TheInventory.GetItemEffect( i ).modToHealth )
				{
					effect = tribComp.TheInventory.GetItemEffect( i );
					currentItemIdx = i;
				}
			}
			if( currentItemIdx != -1 )
				ai.WorkingMemory.SetItem<float>( "closeEnoughDistance", tribComp.TheInventory.GetItem( currentItemIdx ).ItemRange );

		}
		ai.WorkingMemory.SetItem<int>( "currentItemIdxToUse", currentItemIdx );


        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}