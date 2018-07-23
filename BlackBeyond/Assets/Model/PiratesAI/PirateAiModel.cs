using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PirateAiModel
{
    private PirateModel.PirateType pirateType;
    private MapModel map;
    private SpaceModel location;
    protected PirateModel pirateModel;

    protected PirateAiModel(PirateModel.PirateType pirateType, MapModel map)
    {
        this.pirateType = pirateType;
        this.map = map;
    }

    public abstract void EndTurn();

    public PlayerModel GetPlayer()
    {
        List <PathfindingNode> fov = Pathfinding.GetFieldOfView(location, pirateModel.GetDetectRange(), map);
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

    public void EndOfTurn()
    {
        /*END OF TURN METHOD(will be called at end of turn by the turn structure)

In PatrolPath Model class
CONSTRUCTOR
gets the patrol path from pathfinding
END OF TURN */


        //If the pirate is dead check name and create a new one
        //int pirateHealth = pirateModel.GetHealth();
        if (pirateModel == null)
        {
            switch (pirateType)
            {
                case PirateModel.PirateType.Scout:
                    pirateModel = PirateModel.CreateScoutPirate();
                    break;
                case PirateModel.PirateType.Frigate:
                    pirateModel = PirateModel.CreateFrigatePirate();
                    break;
                case PirateModel.PirateType.Platform:
                    pirateModel = PirateModel.CreatePlatformPirate();
                    break;
                case PirateModel.PirateType.Dreadnought:
                    pirateModel = PirateModel.CreateDreadnaughtPirate();
                    break;
                case PirateModel.PirateType.Destroyer:
                    pirateModel = PirateModel.CreateDestroyerPirate();
                    break;
            }
            //if (pirateName.Equals("Scout"))
            //{
            //    PirateModel.CreateScoutPirate();
            //}
            //else if (pirateName.Equals("Frigate"))
            //{
            //    PirateModel.CreateFrigatePirate();
            //}
            //else if (pirateName.Equals("Platform"))
            //{
            //    PirateModel.CreatePlatformPirate();
            //}
            //else if (pirateName.Equals("Destroyer"))
            //{
            //    PirateModel.CreateDestroyerPirate();
            //}
            //else
            //{
            //    PirateModel.CreateDreadnaughtPirate();
            //}
        }
    }
}
