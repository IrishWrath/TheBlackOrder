using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ShipModel
{
    List<PathfindingNode> validMovementSpaces;

    private PlayerController playerController;
    //private SpaceModel playerSpaceModel;

    public SpaceModel playerLocation;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;
    public static int playerHealth = 10;
    public static int playerArmor = 1;

    public int GetHealth()
    {
        return playerHealth;
    }
    public void UpdatePlayerHealth(int health)
    {
        playerHealth = health;
    }

    public int GetArmor()
    {
        return playerArmor;
    }

    public bool playerCanMove = false;

    public PlayerModel(SpaceModel playerSpace)
    {
        //this.playerSpaceModel = playerSpace;
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
        return playerLocation;
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
        playerLocation.LeaveSpace();
        this.playerLocation = location;
        location.OccupySpace(this);
    }

    public bool GetPlayerCanMove()
    {
        return playerCanMove;
    }

    public void SetPlayerCanMove(bool canPlayerMove)
    {
        this.playerCanMove = canPlayerMove;
    }

    // For turn structure
    public void EndTurn()
    {
        // Should block player actions until their turn TODO
        if (validMovementSpaces != null)
        {
            if (validMovementSpaces.Count > 0)
            {
                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }
                validMovementSpaces.Clear();
            }
        }
    }

    public void StartTurn()
    {
        // Unblock player actions TODO

        // reset this player
        currentPlayerMovement = maxPlayerMovement;
        playerController.SetCurrentMovement(currentPlayerMovement, maxPlayerMovement);
    }

    public void StartMove()
    {
        if (!base.animatingMovement)
        {
            if (validMovementSpaces == null || validMovementSpaces.Count == 0)
            {
                // Get all spaces that are valid moves and return into list
                validMovementSpaces = Pathfinding.GetSpacesForMovementDijkstras(playerLocation, currentPlayerMovement);

                SetPlayerCanMove(true);

                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().SetHighlighted(node, this);
                }
            }
            else
            {
                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }

                validMovementSpaces.Clear();
            }
        }
    }

    public void FinishMove(PathfindingNode destination)
    {
        if ((currentPlayerMovement - destination.GetCost()) >= 0 && playerCanMove == true)
        {
            UpdatePlayerLocation(destination.GetSpace());
            UpdateCurrentPlayerMovement(destination.GetCost());

            this.GetController().MoveShip(destination.GetPath(true).ToArray());
            playerController.SetCurrentMovement(currentPlayerMovement, maxPlayerMovement);


            SetPlayerCanMove(false);
            animatingMovement = true;


            foreach (PathfindingNode node in validMovementSpaces)
            {
                node.GetSpace().ClearHighlighted();
            }

            validMovementSpaces.Clear();
        }
    }
}
