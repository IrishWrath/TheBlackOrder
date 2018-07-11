using UnityEngine;

// Model class for a Space
public class SpaceModel 
{
    public SpaceModel(int row, int column, MapModel map)
    {
        Row = row;
        Column = column;
        this.map = map;
        occupied = false;
    }

    public int Row { get; private set; }
    public int Column { get; private set; }

    private readonly MapModel map;
    private SpaceModel[] adjacentSpaces;

    private SpaceController controller;
    private bool occupied;

    // This should be null most of the time. Avoid, outside of pathfinding, these will be null
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
        if(!occupied)
        {
            controller.SetSelectable(cost);
        }
    }
    public void OccupySpace()
    {
        occupied = true;
    }
    public void LeaveSpace()
    {
        occupied = false;
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
