using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationModel
{
    // Shouldn't be here, since this is a manager class.
    //private StationController stationController;
    //private SpaceModel stationSpaceModel;
    

    // Station Resources
    public int currentStationType = 0;
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

    public void createStation(SpaceModel stationLocation, int stationType)
    {
        int reset = 999;

        GenerateStationResources(reset);

        GenerateStationResources(stationType); // IF THE LOADING TIME TAKES TOO LONG, THIS NEEDS TO BE DONE ONLY WHEN A STATION IS DOCKED AT

        Station newStation = new Station(stationLocation, stationType, metalAvailable, metalPrice, organicAvailable, organicPrice, fuelAvailable, fuelPrice, gasAvailable, gasPrice, waterAvailable, waterPrice);

        allStations.Add(stationLocation, newStation);
    }

    public Station GetStation(SpaceModel space)
    {
        return allStations[space];
    }

    private int GetResourceAvailable(int minRange, int maxRange)
    {
        System.Random random = new System.Random();
        int resourceAvailable = random.Next(minRange, maxRange);
        return resourceAvailable;
    }

    private int GetResourcePrice(int resourceAvailable)
    {
        int resourcePrice = 0;

        if(resourceAvailable > 0 && resourceAvailable <=99)
            resourcePrice = 600;//expensive

        else if (resourceAvailable > 99 && resourceAvailable <= 149)
            resourcePrice = 550;

        else if (resourceAvailable > 149 && resourceAvailable <= 199)
            resourcePrice = 500;

        else if (resourceAvailable > 199 && resourceAvailable <= 249)
            resourcePrice = 450;

        else if (resourceAvailable > 249 && resourceAvailable <= 299)
            resourcePrice = 400;

        else if (resourceAvailable > 299 && resourceAvailable <= 349)
            resourcePrice = 350;

        else if (resourceAvailable > 349 && resourceAvailable <= 399)
            resourcePrice = 300;

        else if (resourceAvailable > 399 && resourceAvailable <= 449)
            resourcePrice = 250;

        else if (resourceAvailable > 449 && resourceAvailable <= 499)
            resourcePrice = 200;

        else if (resourceAvailable > 499 && resourceAvailable <= 549)
            resourcePrice = 150;

        else if (resourceAvailable > 549 && resourceAvailable <= 600)
            resourcePrice = 100;

        return resourcePrice;
    }

    private void GenerateStationResources(int station)
    {
        switch(station)
        {
            case 1://General Trade Station
                metalAvailable = GetResourceAvailable(100, 400);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(100, 400);
                organicPrice = GetResourcePrice(organicAvailable);
                fuelAvailable = GetResourceAvailable(100, 400);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(100, 400);
                gasPrice = GetResourcePrice(gasAvailable);
                waterAvailable = GetResourceAvailable(100, 400);
                waterPrice = GetResourcePrice(waterAvailable);
                break;
            case 2://Factory Station
                waterAvailable = GetResourceAvailable(300, 600);
                waterPrice = GetResourcePrice(waterAvailable);
                gasAvailable = GetResourceAvailable(50, 200);
                gasPrice = GetResourcePrice(gasAvailable);
                break;
            case 3://Refinery Station
                fuelAvailable = GetResourceAvailable(300, 600);
                fuelPrice = GetResourcePrice(fuelAvailable);
                gasAvailable = GetResourceAvailable(300, 600);
                gasPrice = GetResourcePrice(gasAvailable);
                metalAvailable = GetResourceAvailable(50, 200);
                metalPrice = GetResourcePrice(metalAvailable);
                break;
            case 4://Mining Station
                metalAvailable = GetResourceAvailable(300, 600);
                metalPrice = GetResourcePrice(metalAvailable);
                organicAvailable = GetResourceAvailable(50, 200);
                organicPrice = GetResourcePrice(organicAvailable);
                break;
            case 5://Farming Station
                organicAvailable = GetResourceAvailable(300, 600);
                organicPrice = GetResourcePrice(organicAvailable);
                waterAvailable = GetResourceAvailable(50, 200);
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

    public int currentStationType = 0;
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

    public Station(SpaceModel stationLocation, int stationType, int newMetalAvail, int newMetalPrice, int newOrganicAvail, int newOrganicPrice, int newFuelAvail, int newFuelPrice, int newGasAvail, int newGasPrice, int newWaterAvail, int newWaterPrice)
    {
        this.currentStation = stationLocation;
        currentStationType = stationType;
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

    public SpaceModel GetStationLocation (Station station)
    {
        return currentStation;
    }
}