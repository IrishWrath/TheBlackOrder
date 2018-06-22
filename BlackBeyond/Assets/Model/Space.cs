using UnityEngine;

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
    private ISpaceCallback callback;

    public PathfindingNode node;

    public ISpaceCallback GetCallback()
    {
        return callback;
    }

    public void SetCallback(ISpaceCallback callback)
    {
        this.callback = callback;
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
