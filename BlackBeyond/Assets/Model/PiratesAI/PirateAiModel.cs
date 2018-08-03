using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Could be any AI. Holds common methods to AIs
public abstract class PirateAiModel
{
    private PirateModel.PirateType pirateType;
    private MapModel map;
    private ModelLink modelLink;
    protected GameController gameController;
    protected PirateModel pirateModel;

    protected PirateAiModel(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, GameController gameController)
    {
        this.pirateType = pirateType;
        this.map = map;
        this.modelLink = modelLink;
        this.gameController = gameController;
        
    }

    public abstract void EndTurn(int turnNumber);

    //public void pursuit()
    //{
    //    PlayerModel player;
    //    int currentSpaceOnPath = 0;
    //    SpaceModel target = player.GetSpace();
    //    PlayerModel playerScan;
    //    List<SpaceModel> targetPath = new List<SpaceModel>();
    //    targetPath.AddRange(AStarPathfinding.GetPathToDestination(pirateModel.GetSpace(), target));

    //    // Oisín Notes: Add a for loop here, and checks for if the player is in range?
    //    for (int i = 0; i < (pirateModel.GetMaxMovement()); i++)
    //    {
    //        {
    //            playerScan = GetPlayerChasing();
    //            if (playerScan != null)
    //            {
    //                i = (pirateModel.GetMaxMovement());
    //            }
    //            else
    //            {
    //                int nextSpace = currentSpaceOnPath + 1;
    //                if (nextSpace == targetPath.Count)
    //                {
    //                    nextSpace = 0;
    //                }
    //                while (targetPath[nextSpace].GetMovementCost() > 99)
    //                {
    //                    i += targetPath[nextSpace].GetNormalMovementCost() - 1;
    //                    nextSpace++;
    //                    if (nextSpace == targetPath.Count)
    //                    {
    //                        nextSpace = 0;
    //                    }
    //                }
    //                i += targetPath[nextSpace].GetMovementCost() - 1;
    //                if (i <= (pirateModel.GetMaxMovement()))
    //                {
    //                    currentSpaceOnPath = nextSpace;
    //                    pirateModel.UpdatePirateLocation(targetPath[currentSpaceOnPath]);
    //                }
    //            }
    //        }
    //        pirateModel.GetController().MoveShip(targetPath, pirateModel, playerScan);
    //    }
    //}

public PlayerModel GetPlayerChasing()
    {
        List<PathfindingNode> fov = Pathfinding.GetFieldOfView(pirateModel.GetSpace(), pirateModel.GetAttackRange(), map);
        foreach (PathfindingNode node in fov)
        {
            if (node.GetSpace().GetPlayer() != null)
            {
                return node.GetSpace().GetPlayer();
            }
        }
        // Outside for loop, no players found
        return null;
    }

    protected MapModel GetMap()
    {
        return map;
    }

    public virtual void NullPirate()
    {
        pirateModel = null;
    }

    public void SpawnPirate(SpaceModel spawnPoint)
    {
        //If the pirate is dead check type and create a new one
        if (pirateModel == null && !spawnPoint.Occupied())
        {
            switch (pirateType)
            {
                case PirateModel.PirateType.Scout:
                    pirateModel = PirateModel.CreateScoutPirate(spawnPoint, this);
                    break;
                case PirateModel.PirateType.Frigate:
                    pirateModel = PirateModel.CreateFrigatePirate(spawnPoint, this);
                    break;
                case PirateModel.PirateType.Platform:
                    pirateModel = PirateModel.CreatePlatformPirate(spawnPoint, this);
                    break;
                case PirateModel.PirateType.Dreadnought:
                    pirateModel = PirateModel.CreateDreadnaughtPirate(spawnPoint, this);
                    break;
                case PirateModel.PirateType.Destroyer:
                    pirateModel = PirateModel.CreateDestroyerPirate(spawnPoint, this);
                    break;
            }

            modelLink.CreatePirateView(pirateModel);
            spawnPoint.OccupySpace(pirateModel);
        }
    }

    public void FinishedMovement()
    {
        gameController.RemovePirateMoving(pirateModel);
    }
}
