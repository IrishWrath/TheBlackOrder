using UnityEngine;

// Model class for a Space
public class Space 
{
    public Space(int row, int column, Map map)
    {
        Row = row;
        Column = column;
        this.map = map;
    }

    public int Row { get; private set; }
    public int Column { get; private set; }

    private readonly Map map;
    private Space[] adjacentSpaces;
    private SpaceController controller;
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
        adjacentSpaces = new Space[6];
        adjacentSpaces[0] = map.GetNE(this);
        adjacentSpaces[1] = map.GetE(this);
        adjacentSpaces[2] = map.GetSE(this);
        adjacentSpaces[3] = map.GetSW(this);
        adjacentSpaces[4] = map.GetW(this);
        adjacentSpaces[5] = map.GetNW(this);
    }

    public Space[] GetAdjacentSpaces()
    {
        return adjacentSpaces;
    }

    public int GetMovementCost()
    {
        // TODO update movement costs
        return 1;
    }

    // Pathfinding End
}
