using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGUIController : MonoBehaviour 
{
    public GameObject text;

    public GameObject dock;

    public GameObject playerFuel;
    public GameObject playerWater;
    public GameObject playerOrganics;
    public GameObject playerMetal;
    public GameObject playerGas;

    public GameObject playerMoney;

    public GameObject fuelCost;
    public GameObject waterCost;
    public GameObject organicsCost;
    public GameObject metalCost;
    public GameObject gasCost;

    private Station station;

    public void ShowUI(int playerFuelNum, int playerWaterNum, int playerOrganicsNum, int playerMetalNum, int playerGasNum, int playerMoneyNum,
                       int fuelCostNum, int waterCostNum, int organicsCostNum, int metalCostNum, int gasCostNum, Station station)
    {
        playerFuel.GetComponent<TextMesh>().text = playerFuelNum.ToString();
        playerWater.GetComponent<TextMesh>().text = playerWaterNum.ToString();
        playerOrganics.GetComponent<TextMesh>().text = playerOrganicsNum.ToString();
        playerMetal.GetComponent<TextMesh>().text = playerMetalNum.ToString();
        playerGas.GetComponent<TextMesh>().text = playerGasNum.ToString();

        playerMoney.GetComponent<TextMesh>().text = playerMoney.ToString();

        fuelCost.GetComponent<TextMesh>().text = "$" + fuelCostNum;
        waterCost.GetComponent<TextMesh>().text = "$" + waterCostNum;
        organicsCost.GetComponent<TextMesh>().text = "$" + organicsCostNum;
        metalCost.GetComponent<TextMesh>().text = "$" + metalCostNum;
        gasCost.GetComponent<TextMesh>().text = "$" + gasCostNum;

        this.station = station;

        dock.SetActive(true);
    }


    // ----------------------------- Buy Methods -----------------------------
    public void BuyFuel()
    {
        // buy fuel, set numbers

        // station.BuyFuel(); // Buys 1 fuel. Make sure this doesn't go above max
        // int fuelCostNum = station.GetFuelCost();
        // int playerFuelNum = station.GetPlayerFuel(); // Station gets from player
        // int playerMoneyNum = station.GetPlayerMoney();

        // playerFuel.GetComponent<TextMesh>().text = playerFuelNum.ToString();
        // fuelCost.GetComponent<TextMesh>().text = "$" + fuelCostNum;
        // playerMoney.GetComponent<TextMesh>().text = playerMoneyNum.ToString();

    }
    public void BuyWater()
    {
        //...
    }
    public void BuyOrganics()
    {
        //...
    }
    public void BuyMetal()
    {
        //...
    }
    public void BuyGas()
    {
        //...
    }

    // ----------------------------- Sell Methods -----------------------------
    public void SellWater()
    {
        // buy Water, set numbers

        // station.SellWater(); // Sells 1 Water. Make sure this method doesn't go below zero

        // int waterCostNum = station.GetWaterPrwater(); 

        // int playerWaterNum = station.GetPlayerWater(); // Station gets from player
        // int playerMoneyNum = station.GetPlayerMoney();

        // playerWater.GetComponent<TextMesh>().text = playerWaterNum.ToString();
        // waterCost.GetComponent<TextMesh>().text = "$" + waterCostNum;
        // playerMoney.GetComponent<TextMesh>().text = playerMoneyNum.ToString();
    }
    public void SellOrganics()
    {
        //...
    }
    public void SellMetal()
    {
        //...
    }
    public void SellGas()
    {
        //...
    }

    // ----------------------------- Other Methods -----------------------------
    public void Undock()
    {
        dock.SetActive(false);
    }
}
