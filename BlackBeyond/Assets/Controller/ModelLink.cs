using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is dedicated to creating links between the View, Controller and Model
// TODO might be better as a static class with static methods
public class ModelLink
{

    public GameController GameController { get; private set; }

    public ModelLink(GameController gameController)
    {
        this.GameController = gameController;
    }

    // Creates the view and gets the controller for a Space
    public void CreateSpaceView(Space space)
    {
        // Creates the space GameObject in the correct position. TODO update this formula for hexes
        GameObject spaceView = Object.Instantiate(GameController.GetSpaceView(), 
                               new Vector2((float)space.Column * 0.6f, (0 - space.Row * 1.04f)), Quaternion.identity);
        // Gets the controller from the GameObject.
        SpaceController controller = spaceView.GetComponent<SpaceController>();
        // Lets the Controller access the GameObject
        controller.SetSpaceView(spaceView);
        // Lets the Controller access the Model
        controller.SetSpace(space);
        // Lets the Model access the Controller, as a callback
        space.SetController(controller);
    }

    // Same as above for a Space GameObject
    public void CreatePlayerView(Player player)
    {
        // Creates the player GameObject in the correct position. TODO update this formula for hexes
        GameObject playerView = Object.Instantiate(GameController.GetPlayerView(),
                                                   player.GetSpace().GetController().GetPosition(), Quaternion.identity);
        // Sets the camera following the player
        Camera.main.transform.parent = playerView.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        // Gets the controller from the GameObject.
        PlayerController controller = playerView.GetComponent<PlayerController>();
        // Lets the Controller access the GameObject
        controller.SetShipView(playerView);
        // Lets the Controller access the Model
        controller.SetModel(player);
        // Lets the Model access the Controller, as a callback
        player.SetController(controller);
    }
}
