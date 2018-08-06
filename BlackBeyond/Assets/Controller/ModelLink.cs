using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is dedicated to creating links between the View, Controller and Model
// TODO might be better as a static class with static methods
public class ModelLink
{

    public GameController GameController { get; private set; }
    public Transform MapContainer { get; private set; }

    public ModelLink(GameController gameController, GameObject mapGameObject)
    {
        this.GameController = gameController;
        this.MapContainer = mapGameObject.transform;
    }

    // Creates the view and gets the controller for a Space
    public void CreateSpaceView(SpaceModel spaceModel)
    {
        // Creates the space GameObject in the correct position. Formula works for hexes
        GameObject spaceView = Object.Instantiate(GameController.GetSpaceView(), 
                                                  new Vector2((float)spaceModel.Column * 0.6f, (0 - spaceModel.Row * 1.04f)), Quaternion.identity, MapContainer);
        // Gets the controller from the GameObject.
        SpaceController spaceController = spaceView.GetComponent<SpaceController>();
        // Lets the Controller access the GameObject
        spaceController.SetSpaceView(spaceView);
        // Lets the Controller access the Model
        spaceController.SetSpace(spaceModel);
        // Lets the Model access the Controller, as a callback
        spaceModel.SetController(spaceController);
    }

    // Creates a Nebula space.
    public void CreateNebulaSpace(NebulaSpaceModel nebulaSpaceModel)
    {
        CreateSpaceView(nebulaSpaceModel);
        Object.Instantiate(GameController.GetNebula(),
                           nebulaSpaceModel.GetController().GetPosition(), Quaternion.identity, MapContainer);
        nebulaSpaceModel.GetController().SetNebula();
    }

    // Creates an asteriod space.
    public void CreateAsteroidSpace(AsteroidSpaceModel asteroidSpaceModel)
    {
        CreateSpaceView(asteroidSpaceModel);
        Object.Instantiate(GameController.GetAsteroid(),
                           asteroidSpaceModel.GetController().GetPosition(), Quaternion.identity, MapContainer);
        asteroidSpaceModel.GetController().SetAsteroid();
    }

    // Same as above for a Space GameObject
    public void CreatePlayerView(PlayerModel playerModel, Text movementText)
    {
        // Creates the player GameObject in the correct position.
        GameObject playerView = Object.Instantiate(GameController.GetPlayerView(),
                                                   playerModel.GetSpace().GetController().GetPosition(), Quaternion.identity);
        // Sets the camera following the player
        Camera.main.transform.parent = playerView.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        // Gets the controller from the GameObject.
        PlayerController playerController = playerView.GetComponentInChildren<PlayerController>();
        // Lets the Controller access the GameObject
        playerController.SetShipView(playerView);
        // Lets the Controller access the Model
        playerController.SetModel(playerModel, GameController.soundController);
        // Lets the Model access the Controller, as a callback
        playerModel.SetController(playerController);

        playerController.SetMovementTextInterface(movementText);
    }

    // Same as above for a Space GameObject
    public void CreatePirateView(PirateModel pirateModel)
    {
        // Creates the player GameObject in the correct position.
        GameObject pirateView = Object.Instantiate(GameController.GetPirateView(),
                                                   pirateModel.GetSpace().GetController().GetPosition(), Quaternion.identity, MapContainer);

        // Gets the controller from the GameObject.
        PirateController pirateController = pirateView.GetComponentInChildren<PirateController>();
        // Lets the Controller access the GameObject
        pirateController.SetShipView(pirateView);
        // Lets the Controller access the Model
        pirateController.SetModel(pirateModel, GameController.soundController);
        // Lets the Model access the Controller, as a callback
        pirateModel.SetController(pirateController);
    }

    // Same as above for a Space GameObject
    public void CreateStationView(StationModel stationModel)
    {
        // Creates the player GameObject in the correct position. TODO update this formula for hexes
        GameObject stationView = Object.Instantiate(GameController.GetStationView(),
                                                   stationModel.GetSpace().GetController().GetPosition(), Quaternion.identity, MapContainer);

        // Gets the controller from the GameObject.
        StationController stationController = stationView.GetComponent<StationController>();
        // Lets the Controller access the GameObject
        stationController.SetStationView(stationView);
        // Lets the Controller access the Model
        stationController.SetModel(stationModel);
        // Lets the Model access the Controller, as a callback
        stationModel.SetController(stationController);
    }
}
