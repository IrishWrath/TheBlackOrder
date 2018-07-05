using UnityEngine;

// Model class for a Space
public class SpaceModel 
{
    public SpaceModel(int row, int column, MapModel map)
    {
        Row = row;
        Column = column;
        this.map = map;
        occupingShip = null;
    }

    public int Row { get; private set; }
    public int Column { get; private set; }

    private readonly MapModel map;
    private SpaceModel[] adjacentSpaces;
    private SpaceController controller;
    private ShipModel occupingShip;
    // This should be null most of the time. Possibly avoid, and use the nodes themselves.
    public PathfindingNode node;

    public SpaceController GetController()
    {
        return controller;
    }

    public void SetController(SpaceController controller)
    {
        this.controller = controller;
    }


    public void WithinMovementRange(int cost)
    {
        if(occupingShip == null)
        {
            controller.SetSelectable(cost);
        }
    }
    public void OccupySpace(ShipModel ship)
    {
        occupingShip = ship;
    }
    public void LeaveSpace()
    {
        occupingShip = null;
    }

    // Astroids damage the ship that moves through them
    public virtual void GetMovementEffects(ShipModel shipModel)
    {
        // Do nothing, not an astroid. In a astroid subclass, the ship will be dealt damage.
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

    public virtual int GetMovementCost()
    {
        // TODO update movement costs
        return 1;
    }

    // Pathfinding End
}
