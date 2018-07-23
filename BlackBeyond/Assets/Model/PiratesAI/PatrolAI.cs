using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAI : PirateAiModel
{
    protected PatrolAI(PirateModel.PirateType pirateType, MapModel map, List<SpaceModel> patrolPoints) : base(pirateType, map)
    {
        // Create Patrol route from patrol points
        // PP 1 -> PP 2 -> PP 3... combined into one list. When it reaches the end, it starts again.
    }

    public override void EndTurn()
    {
        // If Pursuing
            // Pursue
        // else
            // Patrol
    }
}
