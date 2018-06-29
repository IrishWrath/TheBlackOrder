using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    private PlayerController playerController;
    private SpaceModel playerSpaceModel;

    public SpaceModel playerLocation;

    public int maxPlayerMovement = 2;
    public int currentPlayerMovement = 2;

    public PlayerModel(SpaceModel playerSpace)
    {
        this.playerSpaceModel = playerSpace;
        this.playerLocation = playerSpace;
    }

    public PlayerController GetController()
    {
        return playerController;
    }

    public void SetController(PlayerController controller)
    {
        this.playerController = controller;
    }

    public SpaceModel GetSpace()
    {
        return playerSpaceModel;
    }

    public int GetCurrentPlayerMovement()
    {
        return currentPlayerMovement;
    }

    public void UpdateCurrentPlayerMovement(int movementUsed)
    {
        this.currentPlayerMovement = currentPlayerMovement - movementUsed;
    }

    public void UpdatePlayerLocation(SpaceModel location)
    {
        this.playerLocation = location;
    }
}
