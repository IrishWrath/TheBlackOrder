using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ShipModel
{
    List<PathfindingNode> validMovementSpaces;

    private PlayerController playerController;
    private SpaceModel playerSpaceModel;

    public SpaceModel playerLocation;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;

    public bool playerCanMove = false;

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

        if (validMovementSpaces.Count > 0)
        {
            foreach (PathfindingNode node in validMovementSpaces)
            {
                node.GetSpace().ClearHighlighted(node);
            }
            validMovementSpaces.Clear();
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
        if (validMovementSpaces == null || validMovementSpaces.Count == 0)
        {
            // Get all spaces that are valid moves and return into list
            validMovementSpaces = DijkstrasPathfinding.GetSpacesForMovement(playerLocation, currentPlayerMovement);

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
                node.GetSpace().ClearHighlighted(node);
            }

            validMovementSpaces.Clear();
        }
    }

    public void FinishMove(SpaceModel destination)
    {
        int moveCost = 0;
        foreach (PathfindingNode node in validMovementSpaces)
        {
            SpaceModel space = node.GetSpace();

            // find the node for the destination and assign cost of move
            if (space == destination)
            {
                moveCost = node.GetCost();
            }
        }

        if ((currentPlayerMovement - moveCost) >= 0 && playerCanMove == true)
        {
            UpdatePlayerLocation(destination);
            UpdateCurrentPlayerMovement(moveCost);

            this.GetController().MoveShip(destination);
            playerController.SetCurrentMovement(currentPlayerMovement, maxPlayerMovement);


            SetPlayerCanMove(false);

            foreach (PathfindingNode node in validMovementSpaces)
            {
                node.GetSpace().ClearHighlighted(node);
            }

            validMovementSpaces.Clear();
        }
    }
}
