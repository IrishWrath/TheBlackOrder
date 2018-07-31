using UnityEngine;
using System;
using System.Collections;

public class StationController : MonoBehaviour
{
    private GameObject stationView;
    private GameObject dockUI;

    private StationModel stationModel;
    private PlayerModel playerModel;
    private ShipModel shipModel;

    public Sprite defaultSprite;
    public Sprite stationSprite;

    public void SetModel(StationModel stationModel)
    {
        this.stationModel = stationModel;
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

    public void SetStation()
    {
        defaultSprite = stationSprite;
        stationView.GetComponent<SpriteRenderer>().sprite = stationSprite;
    }

    public bool ConfirmCargoSpaceAvailable(int purchaseQuantity)
    {
        if (shipModel.GetAvailableCargoSpace() >= purchaseQuantity)
            return true;

        else
            return false;
    }

    public void ShowDockUI()
    {
        dockUI.SetActive(true);
    }

    public void HideDockUI()
    {
        dockUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
