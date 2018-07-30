using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterKillerAI :PirateAiModel
{
    private List<SpaceModel> targetPath;
    private int currentSpaceOnPath = 0;

    public bool engaged;
    private MapModel map;

    protected HunterKillerAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink) : base(pirateType, map, modelLink)
    {

    }

    public override void EndTurn()
    {   //Joe question: How do I set this method up to get the current location of the player 
        //without an error asking for an object reference?
        SpaceModel target = PlayerModel.GetCurrentLocation();

        targetPath = new List<SpaceModel>();
        targetPath.AddRange(AStarPathfinding.GetPathToDestination(pirateModel.GetSpace(),target));
        base.pirateModel.UpdatePirateLocation(targetPath[currentSpaceOnPath]);
        currentSpaceOnPath++;

    }
}