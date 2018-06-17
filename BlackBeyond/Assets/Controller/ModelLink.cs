using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLink 
{
    private GameObject toSpawn;

    public ModelLink(GameObject toSpawn)
    {
        this.toSpawn = toSpawn;
    }
    public ISpaceCallback CreateSpaceView(int row, int column)
    {
        return SpaceController.Create(row, column, toSpawn);
    }
}
