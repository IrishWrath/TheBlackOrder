using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAI : PirateAiModel
{
    public bool engaged;
    private MapModel map;

    protected PatrolAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, List<SpaceModel> patrolPoints) : base(pirateType, map, modelLink)
    {
        List<SpaceModel> wayPoints = new List<SpaceModel>();
        wayPoints[0] = new SpaceModel(startSpace); //or (ShipModel.GetSpace());
        wayPoints[1] = new SpaceModel(Random.Range(14, 20), Random.Range(14, 20), map);
        wayPoints[2] = new SpaceModel(Random.Range(19, 25), Random.Range(19, 25), map);
        // Create Patrol route from patrol points
        // PP 1 -> PP 2 -> PP 3... combined into one list. When it reaches the end, it starts again.
    }

    public override void EndTurn()
    {
        while (pirateModel.GetCurrentMovement() != 0)
        {
            if (engaged == true)
            {
                PlayerModel target;
                {
                    List<PathfindingNode> fov = Pathfinding.GetFieldOfView(pirateModel.GetSpace(), pirateModel.GetDetectRange() * 3, map);
                    foreach (PathfindingNode node in fov)
                    {
                        if (node.GetSpace().GetPlayer() != null)
                        {
                            target = node.GetSpace().GetPlayer();
                            //insert method to move ship?
                        }
                    }
                    // Outside for loop, no players found
                    engaged = false;
                    // Return to original patrol path
                }
            }


        }

    }
}