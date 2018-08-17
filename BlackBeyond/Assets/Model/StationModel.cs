using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StationModel
{
    public enum StationType
    {
        General = 1, Refinery = 2, Factory = 3, Mining = 4, Planet = 5, Reset = -1
    }

    // Station Resources
    public SpaceModel thisStationLocation;
    public StationType currentStationType;
    public int metalAvailable = 0;
    public int metalPrice = 0;
    public int organicAvailable = 0;
    public int organicPrice = 0;
    public int fuelAvailable = 0;
    public int fuelPrice = 0;
    public int gasAvailable = 0;
    public int gasPrice = 0;
    public int waterAvailable = 0;
    public int waterPrice = 0;

    Dictionary<SpaceModel, Station> allStations = new Dictionary<SpaceModel, Station>();
    private TradeGUIController tradeGUIController;
    private System.Random random;

    public StationModel(TradeGUIController tradeGUIController)
    {
        this.tradeGUIController = tradeGUIController;
        random = new System.Random();
    }

    public void CreateStation(SpaceModel stationLocation, StationType stationType, ModelLink link)
    {
        //int reset = 999;
        GenerateStationResources(StationType.Reset);

        GenerateStationResources(stationType); // IF THE LOADING TIME TAKES TOO LONG, THIS NEEDS TO BE DONE ONLY WHEN A STATION IS DOCKED AT

        Station newStation = new Station(stationLocation, stationType, metalAvailable, metalPrice, organicAvailable, organicPrice, fuelAvailable, fuelPrice, gasAvailable, gasPrice, waterAvailable, waterPrice, this);
        link.CreateStationView(newStation);
        allStations.Add(stationLocation, newStation);
    }

    public TradeGUIController GetTradeGUI()
    {
        return tradeGUIController;
    }

    public Station GetStation(SpaceModel space)
    {
        try
        {
            return allStations[space];
        }
        catch(KeyNotFoundException)
        {
            return null;
        }
    }

    //public void UpdateStation(SpaceModel stationLocation)
    //{
    //    allStations.Remove(stationLocation);

    //    Station newStation = new Station(stationLocation, currentStationType, metalAvailable, metalPrice, organicAvailable, organicPrice, fuelAvailable, fuelPrice, gasAvailable, gasPrice, waterAvailable, waterPrice);
    //}

    private int GetResourceAvailable(int minRange, int maxRange)
    {
        //System.Random random = new System.Random();
        int resourceAvailable = random.Next(minRange, maxRange);
        return resourceAvailable;
    }

    internal int GetResourcePrice(int resourceAvailable)
    {
        int resourcePrice = 0;

        if(resourceAvailable >= 0 && resourceAvailable <= 20)
            resourcePrice = 60;//expensive

        else if (resourceAvailable > 20 && resourceAvailable <= 40)
            resourcePrice = 55;

        else if (resourceAvailable > 40 && resourceAvailable <= 60)
            resourcePrice = 50;

        else if (resourceAvailable > 60 && resourceAvailable <= 80)
            resourcePrice = 45;

        else if (resourceAvailable > 80 && resourceAvailable <= 100)
            resourcePrice = 40;

        else if (resourceAvailable > 100 && resourceAvailable <= 120)
            resourcePrice = 35;

        else if (resourceAvailable > 120 && resourceAvailable <= 140)
            resourcePrice = 30;

        else if (resourceAvailable > 140 && resourceAvailable <= 160)
            resourcePrice = 25;

        else if (resourceAvailable > 160 && resourceAvailable <= 180)
            resourcePrice = 20;

        else if (resourceAvailable > 180 && resourceAvailable <= 200)
            resourcePrice = 15;

        else if (resourceAvailable > 200)
            resourcePrice = 10;

        return resourcePrice;
    }

    private void GenerateStationResources(StationType station)
    {
        switch(station)
        {
            // These Stations trade a variety of things, but don't do much
            case StationType.General://General Trade Station
                metalAvailable = GetResourceAvailable(81, 140);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(81, 140);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(81, 140);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(81, 140);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(81, 140);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            // These gigantic shipyards buy up everything they can. They may have some spare fuel, but it'll come at a price
            case StationType.Factory://Factory Station
                metalAvailable = GetResourceAvailable(0, 100);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(0, 100);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(61, 120);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(0, 100);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(0, 100);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            // These stations suck up gas and refine it into fuel. They need metal for repairs.
            case StationType.Refinery://Refinery Station
                metalAvailable = GetResourceAvailable(15, 60);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(61, 160);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(140, 205);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(140, 210);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(61, 120);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            case StationType.Mining://Mining Station
                metalAvailable = GetResourceAvailable(140, 210);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(15, 60);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(15, 60);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(15, 60);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(15, 60);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            case StationType.Planet://Farming Station
                metalAvailable = GetResourceAvailable(61, 160);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(140, 210);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(15, 60);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(15, 60);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(140, 210);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            default://Default to reset to zero
                metalAvailable = 0;
                metalPrice = 0;
                organicAvailable = 0;
                organicPrice = 0;
                fuelAvailable = 0;
                fuelPrice = 0;
                gasAvailable = 0;
                gasPrice = 0;
                waterAvailable = 0;
                waterPrice = 0;
                break;
        }
    }


    // Station General Trade CASE 1
    // All resources 100 - 400 (if less than 250 then station buy at good price, if more then station selling at good price)

    // Station Factory CASE 2
    // Water - Sell 300 - 600
    // Gas - Buy 50 - 200

    // Station Refinery CASE 3
    // Fuel - Sell 300 - 600
    // Gas - Sell 300 - 600
    // Metal - Buy 50 - 200

    // Station Mining CASE 4
    // Metal - Sell 300 - 600
    // Organic - Buy 50 - 200

    // Station Farm CASE 5
    // Organic - Sell 300 - 600
    // Water - Buy 50 - 200

    // only one StationModel
    //public StationModel(SpaceModel stationSpace)
    //{
    //    this.stationSpaceModel = stationSpace;
    //}

    //public SpaceModel GetSpace()
    //{
    //    return stationSpaceModel;
    //}

    //public StationController GetController()
    //{
    //    return stationController;
    //}

    //public void SetController(StationController controller)
    //{
    //    this.stationController = controller;
    //}
}

public class Station
{
    // This class will have getters for location and stationController, will act like a model class (like PirateModel or SpaceModel)
    private readonly SpaceModel currentStation;
    private StationController stationController;
    private StationModel stationModel;

    public StationModel.StationType stationType;
    public int metalAvailable = 0;
    public int metalPrice = 0;
    public int organicAvailable = 0;
    public int organicPrice = 0;
    public int fuelAvailable = 0;
    public int fuelPrice = 0;
    public int gasAvailable = 0;
    public int gasPrice = 0;
    public int waterAvailable = 0;
    public int waterPrice = 0;
    private PlayerModel player;

    public Station(SpaceModel stationLocation, StationModel.StationType newStationType, int newMetalAvail, int newMetalPrice, int newOrganicAvail, int newOrganicPrice, int newFuelAvail, int newFuelPrice, int newGasAvail, int newGasPrice, int newWaterAvail, int newWaterPrice, StationModel stationModel)
    {
        this.stationModel = stationModel;
        this.currentStation = stationLocation;
        stationType = newStationType;
        metalAvailable = newMetalAvail;
        metalPrice = newMetalPrice;
        organicAvailable = newOrganicAvail;
        organicPrice = newOrganicPrice;
        fuelAvailable = newFuelAvail;
        fuelPrice = newFuelPrice;
        gasAvailable = newGasAvail;
        gasPrice = newGasPrice;
        waterAvailable = newWaterAvail;
        waterPrice = newWaterPrice;
    }

    public void BuyFuel()
    {
        if (fuelAvailable > 0)
        {
            if (player.playerCurrency >= fuelPrice)
            {
                player.playerCurrency -= fuelPrice;
                player.fuelResource += 1;
                fuelAvailable -= 1;
                fuelPrice = stationModel.GetResourcePrice(fuelAvailable);
            }
        }
    }

    public void BuyWater()
    {
        if (waterAvailable > 0)
        {
            if (player.playerCurrency >= waterPrice)
            {
                player.playerCurrency -= waterPrice;
                player.waterResource += 1;
                waterAvailable -= 1;
                waterPrice = stationModel.GetResourcePrice(waterAvailable);
            }
        }
    }
    public void BuyOrganics()
    {
        if (organicAvailable > 0)
        {
            if (player.playerCurrency >= organicPrice)
            {
                player.playerCurrency -= organicPrice;
                player.organicResource += 1;
                organicAvailable -= 1;
                organicPrice = stationModel.GetResourcePrice(organicAvailable);
            }
        }
    }
    public void BuyMetal()
    {
        if (metalAvailable > 0)
        {
            if (player.playerCurrency >= metalPrice)
            {
                player.playerCurrency -= metalPrice;
                player.metalResource += 1;
                metalAvailable -= 1;
                metalPrice = stationModel.GetResourcePrice(metalPrice);
            }
        }
    }
    public void BuyGas()
    {
        if (gasAvailable > 0)
        {
            if (player.playerCurrency >= gasPrice)
            {
                player.playerCurrency -= gasPrice;
                player.gasResource += 1;
                gasAvailable -= 1;
                gasPrice = stationModel.GetResourcePrice(gasAvailable);
            }
        }
    }

    public void SellOrganics()
    {
        if (player.organicResource > 0)
        {
            player.playerCurrency += organicPrice;
            player.organicResource -= 1;
            organicAvailable += 1;
            organicPrice = stationModel.GetResourcePrice(organicAvailable);
        }
    }
    public void SellGas()
    {
        if (player.gasResource > 0)
        {
            player.playerCurrency += gasPrice;
            player.gasResource -= 1;
            gasAvailable += 1;
            gasPrice = stationModel.GetResourcePrice(gasAvailable);
        }
    }
    public void SellMetal()
    {
        if (player.metalResource > 0)
        {
            player.playerCurrency += metalPrice;
            player.metalResource -= 1;
            metalAvailable += 1;
            metalPrice = stationModel.GetResourcePrice(metalAvailable);
        }
    }
    public void SellWater()
    {
        if (player.waterResource > 0)
        {
            player.playerCurrency += waterPrice;
            player.waterResource -= 1;
            waterAvailable += 1;
            waterPrice = stationModel.GetResourcePrice(waterAvailable);
        }
    }


    public int GetPlayerFuel()
    {
        return player.fuelResource;
    }
    public int GetPlayerMoney()
    {
        return player.playerCurrency;
    }
    public int GetPlayerWater()
    {
        return player.waterResource;
    }
    public int GetPlayerOrganics()
    {
        return player.organicResource;
    }
    public int GetPlayerGas()
    {
        return player.gasResource;
    }
    public int GetPlayerMetal()
    {
        return player.metalResource;
    }


    public SpaceModel GetStationLocation (Station station)
    {
        return currentStation;
    }

    public SpaceModel GetSpace()
    {
        return currentStation;
    }

    public void SetController(StationController stationController)
    {
        this.stationController = stationController;
    }

    public StationModel.StationType GetStationType()
    {
        return stationType;
    }

    public int GetMetalAvail()
    {
        return metalAvailable;
    }

    public int GetMetalPrice()
    {
        return metalPrice;
    }

    public int GetOrganicAvail()
    {
        return organicAvailable;
    }

    public int GetOrganicPrice()
    {
        return organicPrice;
    }

    public int GetFuelAvail()
    {
        return fuelAvailable;
    }

    public int GetFuelPrice()
    {
        return fuelPrice;
    }

    public int GetGasAvail()
    {
        return gasAvailable;
    }

    public int GetGasPrice()
    {
        return gasPrice;
    }

    public int GetWaterAvail()
    {
        return waterAvailable;
    }

    public int GetWaterPrice()
    {
        return waterPrice;
    }

    public void ShowDockUI(PlayerModel player)
    {
        this.player = player;
        stationModel.GetTradeGUI().ShowUI(player.fuelResource, player.waterResource, player.organicResource,
                                          player.metalResource, player.gasResource, player.playerCurrency, fuelPrice, waterPrice,
                                          organicPrice, metalPrice, gasPrice, this);
    }
}