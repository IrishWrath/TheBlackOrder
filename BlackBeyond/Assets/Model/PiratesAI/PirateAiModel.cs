using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PirateAiModel
{
    private PirateModel.PirateType pirateType;
    private MapModel map;
    private SpaceModel location = PirateModel.GetSpace();
    protected PirateAiModel(PirateModel.PirateType pirateType, MapModel map)
    {
        this.pirateType = pirateType;
        this.map = map;
    }

    public abstract void EndTurn();

    public PlayerModel GetPlayer()
    {
        List <PathfindingNode> fov = Pathfinding.GetFieldOfView(location, PirateModel.GetDetectRange(), map);
        foreach (blah in fov)
        {
            if (blah.SpaceModel.Player != null)
            {
                return blah.SpaceModel.Player;
            }
        }
        // Outside for loop, no players found
        return null
    }

    protected MapModel GetMap()
    {
        return map;
    }
}
