using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAI : PirateAiModel
{

    // Oisín Notes: Don't need a start space, we can get one from the patrol.
    // SpaceModel startSpace = ShipModel.GetSpace();

    // Oisín Notes: We need a patrol route for this AI, We'll try and get that done before adding pursuit logic
    // Once the path is set up, it never changes, so we'll make it in the constructor
    private List<SpaceModel> patrolPath;
    private int currentSpaceOnPath;

    public bool engaged;
    private MapModel map;

    protected PatrolAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, List<SpaceModel> patrolPoints) : base(pirateType, map, modelLink)
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

        // Create Patrol route from patrol points
        // PP 1 -> PP 2 -> PP 3... combined into one list. When it reaches the end, it starts again.
    }

    public override void EndTurn()
    {
        // Oisín notes: probably call "base.SpawnPirate(patrolPath[0])" around here, so that the pirate appears and respawns
        for (int i = 0; i < (base.pirateModel.GetMaxMovement()); i++) 
        {
            base.GetPlayerChasing();
            // this method, UpdatePirateLocation, should call a move function
            // in PirateController, so that the ships move to their new locations
            base.pirateModel.UpdatePirateLocation(patrolPath[currentSpaceOnPath]);
            currentSpaceOnPath++;
                    // Oisín notes: I would do a for loop (i = 0, i > base.pirateModel.GetMaxMovement(), i++)
                    // inside, check for a player, then move base.pirateModel to the next space on the path with currentSpaceOnPath
                    // also check if we're at the end of the path (currentSpaceOnPath == patrolPath.Count) 
                    // or (currentSpaceOnPath == patrolPath.Count - 1), depending on how you do it.
                    // If we are at the end, go back to the start

            // We don't need the engaged functionality for now, it might be easier to build and perfect 
            // it in the Hunter Killer AI, then copy it in here.
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