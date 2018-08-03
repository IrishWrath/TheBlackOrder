using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterKillerAI :PirateAiModel
{
    private List<SpaceModel> targetPath;
    private int currentSpaceOnPath = 0;
    private SpaceModel target;
    public bool engaged;
    private MapModel map;
    private PlayerModel playerScan;
    private PlayerModel player;

    protected HunterKillerAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, PlayerModel player, GameController gameController) : base(pirateType, map, modelLink, gameController)
    {
        this.player = player;
    }

    public override void EndTurn(int turnNumber)
    { //Defines the path to the player
        target = player.GetSpace();
        targetPath = new List<SpaceModel>();
        targetPath.AddRange(AStarPathfinding.GetPathToDestination(pirateModel.GetSpace(), target));

        // Oisín Notes: Add a for loop here, and checks for if the player is in range?
        for (int i = 0; i < (base.pirateModel.GetMaxMovement()); i++)
        {
            {
                playerScan = base.GetPlayerChasing();
                if (playerScan != null)
                {
                    i = (base.pirateModel.GetMaxMovement());
                }
                else
                {
                    int nextSpace = currentSpaceOnPath + 1;
                    if (nextSpace == targetPath.Count)
                    {
                        nextSpace = 0;
                    }
                    while (targetPath[nextSpace].GetMovementCost() > 99)
                    {
                        i += targetPath[nextSpace].GetNormalMovementCost() - 1;
                        nextSpace++;
                        if (nextSpace == targetPath.Count)
                        {
                            nextSpace = 0;
                        }
                    }
                    i += targetPath[nextSpace].GetMovementCost() - 1;
                    if (i <= (base.pirateModel.GetMaxMovement()))
                    {
                        currentSpaceOnPath = nextSpace;
                        pirateModel.UpdatePirateLocation(targetPath[currentSpaceOnPath]);
                    }
                }
            }
            pirateModel.GetController().MoveShip(targetPath, pirateModel, playerScan);
        }
        targetPath.Clear();
    }
   
}