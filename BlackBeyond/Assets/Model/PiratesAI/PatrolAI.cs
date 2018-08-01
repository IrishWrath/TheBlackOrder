using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAI : PirateAiModel
{
    // Oisín Notes: We need a patrol route for this AI, We'll try and get that done before adding pursuit logic
    // Once the path is set up, it never changes, so we'll make it in the constructor
    private List<SpaceModel> patrolPath;
    private int currentSpaceOnPath;

    public bool engaged;
    private MapModel map;

    public PatrolAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, List<SpaceModel> patrolPoints) : base(pirateType, map, modelLink)
    {
        // Oisín Notes: This constructor takes in a list of patrol points, which we can use to set up the patrol

        patrolPath = new List<SpaceModel>();
        // Oisín Notes: Should be using a for loop, but for now assume that the patrol points have three points.

        // path between point 0 and 1
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[0], patrolPoints[1]));
        // path between point 1 and 2
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[1], patrolPoints[2]));
        // path between point 2 and 0
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[2], patrolPoints[0]));

        // patrolPath should now be one continuous line of spaces. If AStar has bugs, it might break around the edges

        // What space of the path we're on.
        currentSpaceOnPath = 0;
        base.SpawnPirate(patrolPath[0]);

    }

    public override void EndTurn(int turnNumber)
    {
        if(turnNumber % 10 == 0)
        {
            base.SpawnPirate(patrolPath[0]);
        }
        if (pirateModel != null)
        {
            pirateModel.ResetShotCounter();
            PlayerModel player = player = base.GetPlayerChasing();;
            List<SpaceModel> turnPath = new List<SpaceModel>();
            for (int i = 0; i < (base.pirateModel.GetMaxMovement()); i++)
            {
                if (player != null)
                {
                    i = (base.pirateModel.GetMaxMovement());
                }
                else
                {
                    int nextSpace = currentSpaceOnPath + 1;
                    if (nextSpace == patrolPath.Count)
                    {
                        nextSpace = 0;
                    }
                    i += patrolPath[nextSpace].GetMovementCost() - 1;
                    if (i <= (base.pirateModel.GetMaxMovement()))
                    {
                        currentSpaceOnPath = nextSpace;
                        turnPath.Add(patrolPath[currentSpaceOnPath]);
                        pirateModel.UpdatePirateLocation(patrolPath[currentSpaceOnPath]);
                        player = base.GetPlayerChasing();
                    }
                }
            }
            pirateModel.GetController().MoveShip(turnPath, pirateModel, player);
        }
    }

    //if (engaged == true)
    //      {
    //        base.GetPlayerChasing();
    //          // Outside for loop, no players found
    //    
    // Return to original patrol path
    //  else
    //{
    //      engaged = false;

}