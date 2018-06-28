using UnityEngine;
using System.Collections;

public class NebulaSpace: Space
{
    public NebulaSpace(int row, int column, Map map) : base(row, column, map)
    {
    }

    // Nebula movement is 2
    public new int GetMovementCost()
    {
        return 2;
    }
}
