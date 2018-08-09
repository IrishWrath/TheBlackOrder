using UnityEngine;
using System.Collections;
using UnityToolbag;

public class PlatformAi : PirateAiModel
{
    private SpaceModel location;

    public PlatformAi(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, SpaceModel location, GameController gameController) : base(pirateType, map, modelLink, gameController)
    {
        this.location = location;
        base.SpawnPirate(location);
    }

    public override void EndTurn(int turnNumber)
    {
        // only spawns pirate if it is dead
        if (turnNumber % 10 == 0)
        {
            base.SpawnPirate(location);
        }
        if (pirateModel != null)
        {
            pirateModel.ResetShotCounter();
            PlayerModel player = base.GetPlayerChasing();
            if (player != null)
            {
                // gets the model from the superclass, and calls its shoot method.
                // Shoot is a ShipModel method
                Dispatcher.InvokeAsync(() =>
                {
                    base.pirateModel.Shoot(player);
                });
            }
        }
        gameController.RemovePirateMoving();
    }
 
  
}
