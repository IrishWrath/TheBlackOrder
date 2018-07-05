using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ShipModel
{
    private PlayerController playerController;
    private SpaceModel playerSpaceModel;

    public SpaceModel playerLocation;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;

    public PlayerModel(SpaceModel playerSpace)
    {
        this.playerSpaceModel = playerSpace;
        this.playerLocation = playerSpace;
        Debug.Log("Moves Available: " + this.currentPlayerMovement.ToString());
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

    // For turn structure
    public void EndTurn()
    {
        // Should block player actions until their turn TODO
    }

    public void StartTurn()
    {
        // Unblock player actions TODO

        // reset this player
        currentPlayerMovement = maxPlayerMovement;
    }
}
