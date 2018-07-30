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


    public ProceduralMapModel(int rows, int columns, ModelLink link) : base()
    {
        this.rows = rows;
        this.columns = columns;
        map = new SpaceModel[rows][];
        SpaceModel tempSpace;

        /* CREATING SECTORS
         * 0. Nebula Field
         * 1. Asteroid Field
         * 2. Colonized
         * 3. Empty
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
                    //TODO add planet rate

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
                    else if (randomNumber <= nebulaRate + asteroidRate + platformRate)
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace));
                    }
                    else 
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
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
                    //TODO add planet rate

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
                    else if (randomNumber <= nebulaRate + asteroidRate + platformRate)
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace));
                    }
                    else
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
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
    }
}
