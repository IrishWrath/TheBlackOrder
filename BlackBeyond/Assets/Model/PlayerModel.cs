using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ShipModel
{
    // Player trade info
    public int playerCurrency = 0;
    public int metalResource = 0;
    public int organicResource = 0;
    public int fuelResource = 0;
    public int gasResource = 0;
    public int waterResource = 0;

    List<PathfindingNode> validMovementSpaces;

    private PlayerController playerController;

    public bool playerCanMove = false;

    public PlayerModel(SpaceModel currentSpace)
    {
        base.currentSpace = currentSpace;

        // Player stats, TODO
        base.shipArmor = 1;
        base.currentMovement = 3;
        base.maxMovement = 3;
        base.attackRange = 2;
        base.shotDamage = 2;
        base.shipHealth = 10;
        base.maxCargoSpace = 50;
    }

    public PlayerController GetController()
    {
        return playerController;
    }

    public void SetController(PlayerController controller)
    {
        this.playerController = controller;
        base.SetController(controller);
    }

    public int GetCurrentPlayerMovement()
    {
        return base.currentMovement;
    }

    public void UpdateCurrentPlayerMovement(int movementUsed)
    {
        base.currentMovement = base.currentMovement - movementUsed;
    }

    public void UpdatePlayerLocation(SpaceModel location)
    {
        base.currentSpace.LeaveSpace();
        base.currentSpace = location;
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
        base.currentMovement = base.maxMovement;
        playerController.SetCurrentMovement(base.currentMovement, base.maxMovement);
    }

    public void StartMove()
    {
        if (!base.animatingMovement)
        {
            if (validMovementSpaces == null || validMovementSpaces.Count == 0)
            {
                // Get all spaces that are valid moves and return into list
                validMovementSpaces = Pathfinding.GetSpacesForMovementDijkstras(base.currentSpace, base.currentMovement);

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
        if ((base.currentMovement - destination.GetCost()) >= 0 && playerCanMove == true)
        {
            UpdatePlayerLocation(destination.GetSpace());
            UpdateCurrentPlayerMovement(destination.GetCost());

            this.GetController().MoveShip(destination.GetPath(true).ToArray());
            playerController.SetCurrentMovement(base.currentMovement, base.maxMovement);


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
