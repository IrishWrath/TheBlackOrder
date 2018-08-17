using UnityEngine;
using System;
using System.Collections;

public class StationController : MonoBehaviour
{
    private GameObject stationView;
    private GameObject dockUI;

    private Station station;
    private PlayerModel playerModel;
    //private ShipModel shipModel;

    public Sprite generalStation;
    public Sprite miningStation;
    public Sprite refineryStation;
    public Sprite factoryStation;
    public Sprite planetStation;


    public void SetModel(Station station)
    {
        this.station = station;
    }

    public void SetDockUI(GameObject dockUI)
    {
        this.dockUI = dockUI;
    }

    // Sets the view
    public void SetStationView(GameObject stationView)
    {
        this.stationView = stationView;
    }

    // Returns the view's position
    public Vector2 GetPosition()
    {
        return stationView.transform.position;
    }

    public void SetStation(StationModel.StationType stationType)
    {
        switch(stationType)
        {
            case StationModel.StationType.General:
                stationView.GetComponent<SpriteRenderer>().sprite = generalStation;
                break;
            case StationModel.StationType.Factory:
                stationView.GetComponent<SpriteRenderer>().sprite = factoryStation;
                break;
            case StationModel.StationType.Mining:
                stationView.GetComponent<SpriteRenderer>().sprite = miningStation;
                break;
            case StationModel.StationType.Planet:
                stationView.GetComponent<SpriteRenderer>().sprite = planetStation;
                break;
            case StationModel.StationType.Refinery:
                stationView.GetComponent<SpriteRenderer>().sprite = refineryStation;
                break;
        }
    }

    // model method, should be in Station
    //public bool ConfirmCargoSpaceAvailable(int purchaseQuantity)
    //{
    //    if (shipModel.GetAvailableCargoSpace() >= purchaseQuantity)
    //        return true;

    //    else
    //        return false;
    //}

    // TradeGUIController methods
    //public void ShowDockUI()
    //{
    //    dockUI.SetActive(true);
    //}
    //
    //public void HideDockUI()
    //{
    //    dockUI.SetActive(false);
    //}
}
