using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    private PlayerController controller;
    private SpaceModel playerSpace;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;

    public PlayerModel(SpaceModel playerSpace)
    {
        this.playerSpace = playerSpace;
    }

    public PlayerController GetController()
    {
        return controller;
    }

    public void SetController(PlayerController controller)
    {
        this.controller = controller;
    }

    public SpaceModel GetSpace()
    {
        return playerSpace;
    }

    public int getCurrentPlayerMovement()
    {
        return currentPlayerMovement;
    }

    public void updateCurrentPlayerMovement(int movementUsed)
    {
        currentPlayerMovement = currentPlayerMovement - movementUsed;
    }
}
