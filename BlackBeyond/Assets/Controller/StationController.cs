using UnityEngine;
using System;
using System.Collections;

public class StationController : MonoBehaviour
{
    private StationModel stationModel;
    private PlayerModel playerModel;
    private ShipModel shipModel;

    public void SetModel(StationModel stationModel)
    {
        this.stationModel = stationModel;
    }

    public void SetStationView(GameObject stationView)
    {
        throw new NotImplementedException();
    }

    public bool ConfirmCargoSpaceAvailable(int purchaseQuantity)
    {
        if (shipModel.GetAvailableCargoSpace() >= purchaseQuantity)
            return true;

        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
