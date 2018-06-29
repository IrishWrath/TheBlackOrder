using UnityEngine;
using System.Collections;

public class NebulaSpaceModel: SpaceModel
{
    public NebulaSpaceModel(int row, int column, MapModel map) : base(row, column, map)
    {
    }

    // Nebula movement is 2
    public override int GetMovementCost()
    {
        return 2;
    }
}
