using System;
using UnityEngine;

// Model class for a Space
public class SpaceModel 
{
    public enum ClickType
    {
        Move, Shoot, None
    }

    public SpaceModel(int row, int column, MapModel map)
    {
        Row = row;
        Column = column;
        this.map = map;
        occupyingShip = null;
    }

    // don't use player, use occupyingShip. player is for movement and shooting, will be null
    private PlayerModel player;
    private ShipModel occupyingShip;

    public ClickType clickType = ClickType.None;

    public int Row { get; private set; }
    public int Column { get; private set; }

    private readonly MapModel map;
    private SpaceModel[] adjacentSpaces;

    private SpaceController controller;

    // This should be null most of the time. Avoid, outside of pathfinding, these will be null
    public PathfindingNode node;
    // For movement method
    private PathfindingNode moveFunctionNode;

    public SpaceController GetController()
    {
        return controller;
    }

	public void SetController(SpaceController controller)
    {
        this.controller = controller;
    }

    public void OccupySpace(ShipModel ship)
    {
        occupyingShip = ship;
    }
    public void LeaveSpace()
    {
        occupyingShip = null;
    }

    // For Pathfinding

    public void SetNode(PathfindingNode node)
    {
        this.node = node;
    }

    public PathfindingNode GetNode()
    {
        return node;
    }

    public void SetAdjacentSpaces()
    {
        adjacentSpaces = new SpaceModel[6];
        adjacentSpaces[0] = map.GetNE(this);
        adjacentSpaces[1] = map.GetE(this);
        adjacentSpaces[2] = map.GetSE(this);
        adjacentSpaces[3] = map.GetSW(this);
        adjacentSpaces[4] = map.GetW(this);
        adjacentSpaces[5] = map.GetNW(this);
    }

    public SpaceModel[] GetAdjacentSpaces()
    {
        return adjacentSpaces;
    }

    // Asteroids damage the ship that moves through them
    public virtual void GetMovementEffects(ShipModel shipModel)
    {
        // Do nothing, not an asteroid. In a asteroid subclass, the ship will be dealt damage.
    }

    public virtual int GetMovementCost()
    {
        if(occupyingShip == null)
        {
            return 1;
        }
        else
        {
            // impassable
            return 100;
        }
    }

    public virtual int GetNormalMovementCost()
    {
        return 1;
    }

    public virtual bool IsHazardous()
    {
        return false;
    }

    // Pathfinding End

    public void SetHighlighted(PathfindingNode node, PlayerModel player)
    {
        
        if (occupyingShip == null)
        {
            this.player = player;
            this.GetController().SetSelectable(node.GetCost());
            this.moveFunctionNode = node;
            this.clickType = ClickType.Move;
        }
    }

    public void SetShootHighlighted(PathfindingNode node, PlayerModel playerModel)
    {
        this.player = playerModel;
        this.GetController().SetShootable(occupyingShip != null);
        this.clickType = ClickType.Shoot;
    }

    public void ClearHighlighted()
    {
        this.GetController().Deselect();
        player = null;
        moveFunctionNode = null;
        this.clickType = ClickType.None;
    }

    public void Clicked()
    {
        switch(clickType)
        {
            case ClickType.Move:
                player.FinishMove(moveFunctionNode);
                break;
            case ClickType.Shoot:
                player.FinishShoot(occupyingShip);
                break;
            case ClickType.None:
                break;
        }
    }

    public virtual bool BlocksLOS()
    {
        return false;
    }

    public PlayerModel GetPlayer()
    {
        if (occupyingShip != null)
        {
            if (occupyingShip.GetType() == typeof(PlayerModel))
            {
                return (PlayerModel)occupyingShip;
            }
        }
        return null;
    }
}
