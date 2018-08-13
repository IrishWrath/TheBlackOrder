using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGUIController : MonoBehaviour 
{
    public GameObject text;

    public GameObject dock;

    public GameObject playerFuel;
    public GameObject playerIce;
    public GameObject playerOrganics;
    public GameObject playerMetal;
    public GameObject playerGas;

    public GameObject fuelCost;
    public GameObject iceCost;
    public GameObject organicsCost;
    public GameObject metalCost;
    public GameObject gasCost;


    public void ShowUI(int player)
    {
        
    }

    public void BuyFuel()
    {

    }
    public void BuyIce()
    {

    }
    public void BuyOrganics()
    {

    }
    public void BuyMetal()
    {

    }
    public void BuyGas()
    {
        
    }

    public void SellIce()
    {

    }
    public void SellOrganics()
    {

    }
    public void SellMetal()
    {

    }
    public void SellGas()
    {

    }

    public void Undock()
    {
        dock.SetActive(false);
    }
}
