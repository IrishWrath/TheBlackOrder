using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbag;

public class HunterKillerAI :PirateAiModel
{
    //private List<SpaceModel> targetPath;
    //private int currentSpaceOnPath = 0;
    private SpaceModel target;
    public bool engaged;
    //private PlayerModel playerScan;
    private PlayerModel player;

    public HunterKillerAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, PlayerModel player, GameController gameController) : base(pirateType, map, modelLink, gameController)
    {
        this.player = player;
    }

    public override void EndTurn(int turnNumber)
    {
        if (turnNumber % 10 == 0)
        {
            base.SpawnPirate(map.GetRandomSpace());
        }

        if (pirateModel != null)
        {
            pirateModel.ResetShotCounter();
            int currentSpaceOnPath = -1;
            //Defines the path to the player
            target = player.GetSpace();
            List<SpaceModel> targetPath = AStarPathfinding.GetPathToDestination(pirateModel.GetSpace(), target);
            List<SpaceModel> turnPath = new List<SpaceModel>();

            PlayerModel playerScan = base.GetPlayerChasing();

            // Oisín Notes: Add a for loop here, and checks for if the player is in range?
            for (int i = 0; i < (base.pirateModel.GetMaxMovement()); i++)
            {
                if (playerScan != null)
                {
                    break;
                }
                else
                {
                    int nextSpace = currentSpaceOnPath + 1;
                    if (nextSpace == targetPath.Count - 1)
                    {
                        break;
                    }
                    bool breakOutofLoop = false;
                    while (targetPath[nextSpace].GetMovementCost() > 99)
                    {
                        i += targetPath[nextSpace].GetNormalMovementCost() - 1;
                        nextSpace++;
                        breakOutofLoop |= nextSpace == targetPath.Count - 1;
                    }
                    if (breakOutofLoop)
                    {
                        break;
                    }
                    i += targetPath[nextSpace].GetMovementCost() - 1;
                    if (i <= (base.pirateModel.GetMaxMovement()))
                    {
                        currentSpaceOnPath = nextSpace;
                        pirateModel.UpdatePirateLocation(targetPath[currentSpaceOnPath]);
                        turnPath.Add(targetPath[currentSpaceOnPath]);
                        playerScan = base.GetPlayerChasing();
                    }
                }
            }
            Dispatcher.InvokeAsync(() =>
            {
                pirateModel.GetController().MoveShip(turnPath, pirateModel, playerScan);
                foreach (SpaceModel space in turnPath)
                {
                    space.GetMovementEffects(pirateModel);
                }
            });
        }
        else
        {
            gameController.RemovePirateMoving();
        }
    }
}