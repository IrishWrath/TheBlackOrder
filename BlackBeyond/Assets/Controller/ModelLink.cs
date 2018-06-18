using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLink : IModelLink
{
    private GameObject SpaceView;

    public ModelLink(GameObject spaceView)
    {
        this.SpaceView = spaceView;
    }
    public ISpaceCallback CreateSpaceView(int row, int column)
    {
        return SpaceController.Create(row, column, SpaceView);
    }
}
