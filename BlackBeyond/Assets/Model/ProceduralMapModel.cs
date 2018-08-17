using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapModel : MapModel
{
    //private readonly SpaceModel[][] map;
    //private int rows;
    //private int columns;

    //private List<PirateAiModel> pirates = new List<PirateAiModel>();

    //public static readonly int NEBULA_RATE = 1, ASTEROID_RATE = NEBULA_RATE + 1, PLATFORM_RATE = 2;

    public static readonly int SECTOR_SIZE = 25;
    private int[,] sectors;


    public ProceduralMapModel(int rows, int columns, ModelLink link, GameController gameController, StationModel stationModel) : base()
    {
        base.stationModel = stationModel;
        base.gameController = gameController;
        base.link = link;
        this.rows = rows;
        this.columns = columns;
        map = new SpaceModel[rows][];
        SpaceModel tempSpace;

        /* CREATING SECTORS
         * 0. Nebula Field
         * 1. Asteroid Field
         * 2. Colonized
         * 3. Empty
         * 4. Pirate base
         */

        sectors = new int[rows/SECTOR_SIZE, columns/SECTOR_SIZE/2];

        for (int x = 0; x < rows/SECTOR_SIZE; x++)
        {
            for (int y = 0; y < columns / SECTOR_SIZE / 2; y++)
            {
                int sectorType = UnityEngine.Random.Range(0, 4);
                sectors[x, y] = sectorType;
            }
        }
        //make the middle spot a colonized area.
        sectors[2, 2] = 3;

        int randomAxis = UnityEngine.Random.Range(0, 2);
        int randomSide = UnityEngine.Random.Range(0, 2);
        int position = UnityEngine.Random.Range(0, 5);
        if (randomAxis == 0)
        {
            // pirate base is on top or bottom
            sectors[position, randomSide * 4] = 4;
        } 
        else
        {
            // pirate base is on left or right
            sectors[randomSide * 4, position] = 4;
        }
        //Debug.Log(randomSide + " : " + randomAxis);
            

        for (int row = 0; row < rows; row++)
        {
            map[row] = new SpaceModel[columns];
            if(row%2==0)
            {
                for (int column = 0; column < columns; column += 2)
                {
                    int sectorType = sectors[row / SECTOR_SIZE, column/SECTOR_SIZE / 2];

                    //rates (out of 625)
                    int nebulaRate = 100;
                    int asteroidRate = 100;
                    int platformRate = 1;
                    //empty space makes up the rest

                    int stationRate = 5;
                    int refineryRate = 20;
                    int miningRate = 20;
                    int generalRate = 20;
                    int planetRate = 20;
                    int factoryRate = 20;

                    switch (sectorType)
                    {
                        case 0:
                            //Nebula Field
                            nebulaRate = 150;
                            asteroidRate = 50;
                            platformRate = 1;

                            break;
                        case 1:
                            //Asteroid Field
                            nebulaRate = 50;
                            asteroidRate = 150;
                            platformRate = 1;

                            break;
                        case 2:
                            // Empty
                            nebulaRate = 30;
                            asteroidRate = 30;
                            platformRate = 2;

                            break;
                        case 3:
                            //colonised
                            nebulaRate = 50;
                            asteroidRate = 50;
                            platformRate = 1;
                            stationRate = 7;

                            break;
                        case 4:
                            //pirate base
                            nebulaRate = 100;
                            asteroidRate = 100;
                            platformRate = 50;
                            stationRate = 0;
                            break;

                        default:
                            break;
                    }

                    int randomNumber = UnityEngine.Random.Range(1, 625);
                    if(randomNumber <= nebulaRate)
                    {
                        tempSpace = new NebulaSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpaceModel)tempSpace);
                    }
                    else if (randomNumber <= nebulaRate + asteroidRate)
                    {
                        tempSpace = new AsteroidSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateAsteroidSpace((AsteroidSpaceModel)tempSpace);
                    }
                    else 
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                    }

                    //see if there are pirates
                    randomNumber = UnityEngine.Random.Range(1, 625);

                    if (randomNumber <= platformRate)
                    {
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace, gameController));
                    }

                    if (randomNumber > platformRate && randomNumber <= platformRate + stationRate)
                    {
                        int randomType = UnityEngine.Random.Range(1, generalRate + refineryRate + miningRate + planetRate + factoryRate + 1);
                        StationModel.StationType type;
                        if (randomType < generalRate)
                        {
                            type = StationModel.StationType.General;
                        }
                        else if(randomType < generalRate + refineryRate)
                        {
                            type = StationModel.StationType.Refinery;
                        }
                        else if (randomType < generalRate + refineryRate + miningRate)
                        {
                            type = StationModel.StationType.Mining;
                        }
                        else if (randomType < generalRate + refineryRate + miningRate + planetRate)
                        {
                            type = StationModel.StationType.Planet;
                        }
                        else
                        {
                            type = StationModel.StationType.Factory;
                        }
                        stationModel.CreateStation(tempSpace, type, link);
                    }

                }
            }
            else
            {
                for (int column = 1; column < columns; column += 2)
                {
                    int sectorType = sectors[row / SECTOR_SIZE, column / SECTOR_SIZE / 2];

                    //rates (out of 625)
                    int nebulaRate = 100;
                    int asteroidRate = 100;
                    int platformRate = 1;
                    //empty space makes up the rest

                    int stationRate = 5;
                    int refineryRate = 20;
                    int miningRate = 20;
                    int generalRate = 20;
                    int planetRate = 20;
                    int factoryRate = 20;

                    switch (sectorType)
                    {
                        case 0:
                            //Nebula Field
                            nebulaRate = 150;
                            asteroidRate = 50;
                            platformRate = 1;

                            break;
                        case 1:
                            //Asteroid Field
                            nebulaRate = 50;
                            asteroidRate = 150;
                            platformRate = 1;

                            break;
                        case 2:
                            // Empty
                            nebulaRate = 30;
                            asteroidRate = 30;
                            platformRate = 2;

                            break;
                        case 3:
                            //colonised
                            nebulaRate = 50;
                            asteroidRate = 50;
                            platformRate = 1;
                            stationRate = 7;

                            break;
                        case 4:
                            //pirate base
                            nebulaRate = 100;
                            asteroidRate = 100;
                            platformRate = 50;
                            stationRate = 0;
                            break;

                        default:
                            break;
                    }

                    int randomNumber = UnityEngine.Random.Range(1, 625);
                    if (randomNumber <= nebulaRate)
                    {
                        tempSpace = new NebulaSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpaceModel)tempSpace);
                    }
                    else if (randomNumber <= nebulaRate + asteroidRate)
                    {
                        tempSpace = new AsteroidSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateAsteroidSpace((AsteroidSpaceModel)tempSpace);
                    }
                    else
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                    }

                    //see if there are pirates
                    randomNumber = UnityEngine.Random.Range(1, 625);

                    if (randomNumber <= platformRate)
                    {
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace, gameController));
                    }

                    if (randomNumber > platformRate && randomNumber <= platformRate + stationRate)
                    {
                        int randomType = UnityEngine.Random.Range(1, generalRate + refineryRate + miningRate + planetRate + factoryRate + 1);
                        StationModel.StationType type;
                        if (randomType < generalRate)
                        {
                            type = StationModel.StationType.General;
                        }
                        else if (randomType < generalRate + refineryRate)
                        {
                            type = StationModel.StationType.Refinery;
                        }
                        else if (randomType < generalRate + refineryRate + miningRate)
                        {
                            type = StationModel.StationType.Mining;
                        }
                        else if (randomType < generalRate + refineryRate + miningRate + planetRate)
                        {
                            type = StationModel.StationType.Planet;
                        }
                        else
                        {
                            type = StationModel.StationType.Factory;
                        }
                        stationModel.CreateStation(tempSpace, type, link);
                    }

                }
            }
        }
        // Setting each space's adjacent spaces. Must be done after the map is generated
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if(map[row][column] != null)
                {
                    map[row][column].SetAdjacentSpaces();
                }
            }
        }



        for (int i = 0; i < 50; i++)
        {
            List<SpaceModel> patrolPoints = new List<SpaceModel>();
            for (int j = 0; j < 3; j++)
            {
                patrolPoints.Add(GetRandomSpace());
            }
            pirates.Add(new PatrolAI(PirateModel.PirateType.Scout, this, link, patrolPoints, gameController));
        }

        for (int i = 0; i < 35; i++)
        {
            List<SpaceModel> patrolPoints = new List<SpaceModel>();
            for (int j = 0; j < 3; j++)
            {
                patrolPoints.Add(GetRandomSpace());
            }

            pirates.Add(new PatrolAI(PirateModel.PirateType.Frigate, this, link, patrolPoints, gameController));
        }

        for (int i = 0; i < 10; i++)
        {
            List<SpaceModel> patrolPoints = new List<SpaceModel>();
            for (int j = 0; j < 3; j++)
            {
                patrolPoints.Add(GetRandomSpace());
            }
            pirates.Add(new PatrolAI(PirateModel.PirateType.Destroyer, this, link, patrolPoints, gameController));
        }

        for (int i = 0; i < 5; i++)
        {
            List<SpaceModel> patrolPoints = new List<SpaceModel>();
            for (int j = 0; j < 3; j++)
            {
                patrolPoints.Add(GetRandomSpace());
            }
            pirates.Add(new PatrolAI(PirateModel.PirateType.Dreadnought, this, link, patrolPoints, gameController));
        }
    }
}
