using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    private PlayerController playerController;
    private SpaceModel playerSpaceModel;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;
    public static int playerHealth = 10;
    public static int playerArmor = 2;

    public static int GetHealth()
    {
        return playerHealth;
    }
    public static void UpdatePlayerHealth(int health)
    {
        playerHealth = health;
    }

    public static int GetArmor()
    {
        return playerArmor;
    }

    public PlayerModel(SpaceModel playerSpace)
    {
        this.playerSpaceModel = playerSpace;
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
        currentPlayerMovement = currentPlayerMovement - movementUsed;
    }
}
