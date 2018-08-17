using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        playerFuel.GetComponent<Text>().text = playerFuelNum.ToString();
        playerWater.GetComponent<Text>().text = playerWaterNum.ToString();
        playerOrganics.GetComponent<Text>().text = playerOrganicsNum.ToString();
        playerMetal.GetComponent<Text>().text = playerMetalNum.ToString();
        playerGas.GetComponent<Text>().text = playerGasNum.ToString();

        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();

        fuelCost.GetComponent<Text>().text = "$" + fuelCostNum;
        waterCost.GetComponent<Text>().text = "$" + waterCostNum;
        organicsCost.GetComponent<Text>().text = "$" + organicsCostNum;
        metalCost.GetComponent<Text>().text = "$" + metalCostNum;
        gasCost.GetComponent<Text>().text = "$" + gasCostNum;

        this.station = station;

        dock.SetActive(true);
    }


    // ----------------------------- Buy Methods -----------------------------
    public void BuyFuel()
    {
        // buy fuel, set numbers

        station.BuyFuel(); // Buys 1 fuel. Make sure this doesn't go above max
        int fuelCostNum = station.GetFuelPrice();
        int playerFuelNum = station.GetPlayerFuel(); // Station gets from player
        int playerMoneyNum = station.GetPlayerMoney();

        playerFuel.GetComponent<Text>().text = playerFuelNum.ToString();
        fuelCost.GetComponent<Text>().text = "$" + fuelCostNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();

    }
    public void BuyWater()
    {
        station.BuyWater(); 
        int costNum = station.GetWaterPrice();
        int resourceNum = station.GetPlayerWater(); 
        int playerMoneyNum = station.GetPlayerMoney();

        playerWater.GetComponent<Text>().text = resourceNum.ToString();
        waterCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyOrganics()
    {
        station.BuyOrganics();
        int costNum = station.GetOrganicPrice();
        int resourceNum = station.GetPlayerOrganics();
        int playerMoneyNum = station.GetPlayerMoney();

        playerOrganics.GetComponent<Text>().text = resourceNum.ToString();
        organicsCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyMetal()
    {
        station.BuyMetal();
        int costNum = station.GetMetalPrice();
        int resourceNum = station.GetPlayerMetal();
        int playerMoneyNum = station.GetPlayerMoney();

        playerMetal.GetComponent<Text>().text = resourceNum.ToString();
        metalCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyGas()
    {
        station.BuyGas();
        int costNum = station.GetGasPrice();
        int resourceNum = station.GetPlayerGas();
        int playerMoneyNum = station.GetPlayerMoney();

        playerGas.GetComponent<Text>().text = resourceNum.ToString();
        gasCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }

    // ----------------------------- Sell Methods -----------------------------
    public void SellWater()
    {
        // buy Water, set numbers

        station.SellWater(); // Sells 1 Water. Make sure this method doesn't go below zero

        int waterCostNum = station.GetWaterPrice(); 

        int playerWaterNum = station.GetPlayerWater(); // Station gets from player
        int playerMoneyNum = station.GetPlayerMoney();

        playerWater.GetComponent<Text>().text = playerWaterNum.ToString();
        waterCost.GetComponent<Text>().text = "$" + waterCostNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellOrganics()
    {
        station.SellOrganics();
        int costNum = station.GetOrganicPrice();
        int resourceNum = station.GetPlayerOrganics();
        int playerMoneyNum = station.GetPlayerMoney();

        playerOrganics.GetComponent<Text>().text = resourceNum.ToString();
        organicsCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellMetal()
    {
        station.SellMetal();
        int costNum = station.GetMetalPrice();
        int resourceNum = station.GetPlayerMetal();
        int playerMoneyNum = station.GetPlayerMoney();

        playerMetal.GetComponent<Text>().text = resourceNum.ToString();
        metalCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellGas()
    {
        station.SellGas();
        int costNum = station.GetGasPrice();
        int resourceNum = station.GetPlayerGas();
        int playerMoneyNum = station.GetPlayerMoney();

        playerGas.GetComponent<Text>().text = resourceNum.ToString();
        gasCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }

    // ----------------------------- Other Methods -----------------------------
    public void Undock()
    {
        dock.SetActive(false);
    }
}
