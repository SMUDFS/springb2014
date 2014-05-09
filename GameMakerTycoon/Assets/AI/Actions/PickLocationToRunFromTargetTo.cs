using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class PickLocationToRunFromTargetTo : RAINAction
{
    public PickLocationToRunFromTargetTo()
    {
        actionName = "PickLocationToRunFromTargetTo";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		//if( ai.Motor.IsAtMoveTarget )
		{
			//trying to set this variable: runAwayLocation
			Vector3 runAwayLocation = ai.Body.gameObject.transform.position;
			GameObject targetTribute = ai.WorkingMemory.GetItem<GameObject>( "varTribute" );
			if( targetTribute != null )
			{
				Vector3 deltaToTarget = ai.Body.gameObject.transform.position - targetTribute.gameObject.transform.position;
				runAwayLocation += deltaToTarget.normalized * ( ai.Senses.GetSensor( "ears" ) as RAIN.Perception.Sensors.AudioSensor ).Range;
				runAwayLocation.y = ai.Body.gameObject.transform.position.y;
				ai.WorkingMemory.SetItem<Vector3>( "runAwayLocation", runAwayLocation );
				Debug.Log( "Choosing run away location" );
			}

		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}