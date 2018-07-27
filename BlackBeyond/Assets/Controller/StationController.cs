using UnityEngine;
using System;
using System.Collections;

public class StationController : MonoBehaviour
{
    private StationModel stationModel;

    public void SetModel(StationModel stationModel)
    {
        this.stationModel = stationModel;
    }

    public void SetStationView(GameObject stationView)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
