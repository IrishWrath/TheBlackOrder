using UnityEngine;
using System.Collections;

public class PlatformAi : PirateAiModel
{
    private SpaceModel location;

    protected PlatformAi(PirateModel.PirateType pirateType, MapModel map, SpaceModel location) : base(pirateType, map)
    {
        this.location = location;
    }

    public override void EndTurn()
    {
        // If player in range
            // Shoot
    }
}
