using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLink : IModelLink
{

    public GameController GameController { get; private set; }

    public ModelLink(GameController gameController)
    {
        this.GameController = gameController;
    }

    public void CreateSpaceView(Space space)
    {
        GameObject spaceView = Object.Instantiate(GameController.GetSpaceView(), 
                               new Vector2(((float)space.Column - 1) / 2, (0 - space.Row)), Quaternion.identity);
        SpaceController controller = spaceView.GetComponent<SpaceController>();
        controller.spaceView = spaceView;
        controller.SetSpace(space);
        space.SetCallback(controller);
    }

    public void CreatePlayerView(Player player)
    {
        GameObject playerView = Object.Instantiate(GameController.GetPlayerView(),
                                                   new Vector2(((float)player.GetSpace().Column - 1) / 2, (0 - player.GetSpace().Row)), Quaternion.identity);
        PlayerController controller = playerView.GetComponent<PlayerController>();
        controller.SetShipView(playerView);
        controller.SetModel(player);
        player.SetCallback(controller);
    }
}
