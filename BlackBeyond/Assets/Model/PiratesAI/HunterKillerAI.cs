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

    private PlayerModel player;

    protected HunterKillerAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, PlayerModel player) : base(pirateType, map, modelLink)
    {
        this.player = player;
    }

    public override void EndTurn()
    {
        target = player.GetSpace();

        // Oisín Notes: Add a for loop here, and checks for if the player is in range?
        for (int i = 0; i < (base.pirateModel.GetMaxMovement()); i++)
        {
            base.GetPlayerChasing();
            targetPath = new List<SpaceModel>();
            targetPath.AddRange(AStarPathfinding.GetPathToDestination(pirateModel.GetSpace(), target));
            base.pirateModel.UpdatePirateLocation(targetPath[currentSpaceOnPath]);
            currentSpaceOnPath++;
        }
    }
}