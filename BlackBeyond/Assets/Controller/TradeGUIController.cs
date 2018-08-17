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

    private SoundController soundController;
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
        soundController.SwitchMusic(SoundController.Sound.trade);
    }


    // ----------------------------- Buy Methods -----------------------------
    public void BuyFuel()
    {
        // buy fuel, set numbers

        if (station.BuyFuel()){ // Buys 1 fuel. Make sure this doesn't go above max
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int fuelCostNum = station.GetFuelPrice();
        int playerFuelNum = station.GetPlayerFuel(); // Station gets from player
        int playerMoneyNum = station.GetPlayerMoney();

        playerFuel.GetComponent<Text>().text = playerFuelNum.ToString();
        fuelCost.GetComponent<Text>().text = "$" + fuelCostNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
        

    }
    public void BuyWater()
    {
        if (station.BuyWater())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int costNum = station.GetWaterPrice();
        int resourceNum = station.GetPlayerWater(); 
        int playerMoneyNum = station.GetPlayerMoney();

        playerWater.GetComponent<Text>().text = resourceNum.ToString();
        waterCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyOrganics()
    {
        if (station.BuyOrganics())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int costNum = station.GetOrganicPrice();
        int resourceNum = station.GetPlayerOrganics();
        int playerMoneyNum = station.GetPlayerMoney();

        playerOrganics.GetComponent<Text>().text = resourceNum.ToString();
        organicsCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyMetal()
    {
        if (station.BuyMetal())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int costNum = station.GetMetalPrice();
        int resourceNum = station.GetPlayerMetal();
        int playerMoneyNum = station.GetPlayerMoney();

        playerMetal.GetComponent<Text>().text = resourceNum.ToString();
        metalCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void BuyGas()
    {
        if (station.BuyGas())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
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
        // sell* Water, set numbers

        if (station.SellWater())
        { // Sells 1 Water. Make sure this method doesn't go below zero
            soundController.PlaySound(SoundController.Sound.buy);
        }

        int waterCostNum = station.GetWaterPrice(); 

        int playerWaterNum = station.GetPlayerWater(); // Station gets from player
        int playerMoneyNum = station.GetPlayerMoney();

        playerWater.GetComponent<Text>().text = playerWaterNum.ToString();
        waterCost.GetComponent<Text>().text = "$" + waterCostNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellOrganics()
    {
        if (station.SellOrganics())
        { 
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int costNum = station.GetOrganicPrice();
        int resourceNum = station.GetPlayerOrganics();
        int playerMoneyNum = station.GetPlayerMoney();

        playerOrganics.GetComponent<Text>().text = resourceNum.ToString();
        organicsCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellMetal()
    {
        if (station.SellMetal())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
        int costNum = station.GetMetalPrice();
        int resourceNum = station.GetPlayerMetal();
        int playerMoneyNum = station.GetPlayerMoney();

        playerMetal.GetComponent<Text>().text = resourceNum.ToString();
        metalCost.GetComponent<Text>().text = "$" + costNum;
        playerMoney.GetComponent<Text>().text = playerMoneyNum.ToString();
    }
    public void SellGas()
    {
        if (station.SellGas())
        {
            soundController.PlaySound(SoundController.Sound.buy);
        }
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
        soundController.SwitchMusic(SoundController.Sound.main);
    }

    public void SetSoundController(SoundController soundController)
    {
        this.soundController = soundController;
    }
}
