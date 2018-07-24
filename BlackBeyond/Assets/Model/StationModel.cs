using UnityEngine;
using System.Collections;

public class StationModel
{
    private StationController stationController;
    private SpaceModel stationSpaceModel;

    public StationModel(SpaceModel stationSpace)
    {
        this.stationSpaceModel = stationSpace;
    }

    public SpaceModel GetSpace()
    {
        return stationSpaceModel;
    }

    public void SetController(StationController controller)
    {
        this.stationController = controller;
    }

}
