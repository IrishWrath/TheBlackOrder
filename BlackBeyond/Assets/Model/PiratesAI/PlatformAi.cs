using UnityEngine;
using System.Collections;

public class PlatformAi : PirateAiModel
{
    private SpaceModel location;

    public PlatformAi(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, SpaceModel location) : base(pirateType, map, modelLink)
    {
        this.location = location;
    }

    public override void EndTurn()
    {
        base.EndOfTurn(location);
        PlayerModel player = base.GetPlayer();
        if (player != null)
        {
            base.pirateModel.Shoot(player);
        }
    }
 
  
}
