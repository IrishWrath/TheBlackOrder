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
        PlayerModel player = base.GetPlayer();
        if (player != null)
        {
            PirateModel.Shoot();
        }
    }
 
  
}
