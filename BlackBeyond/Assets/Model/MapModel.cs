using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel 
{
    private readonly SpaceModel[][] map;
    private int rows;
    private int columns;

    private List<PirateAiModel> pirates = new List<PirateAiModel>();

    // Create a map. TODO nothing in this map yet.
    public MapModel(int rows, int columns, ModelLink link)
    {
        this.rows = rows;
        this.columns = columns;
        map = new SpaceModel[rows][];
        SpaceModel tempSpace;
        for (int row = 0; row < rows; row++)
        {
            map[row] = new SpaceModel[columns];
            if(row%2==0)
            {
                for (int column = 0; column < columns; column += 2)
                {
                    int randomNumber = UnityEngine.Random.Range(1, 11);
                    if(randomNumber <= 1)
                    {
                        tempSpace = new NebulaSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpaceModel)tempSpace);
                    }
                    else if (randomNumber <= 2)
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
                    if(UnityEngine.Random.Range(1,101) == 1)
                    {
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace));
                    }

                    if (UnityEngine.Random.Range(1, 101) == 1)
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateStationView((StationModel)tempSpace);
                    }
                }
            }
            else
            {
                for (int column = 1; column < columns; column += 2)
                {
                    int randomNumber = UnityEngine.Random.Range(1, 11);
                    if (randomNumber <= 1)
                    {
                        tempSpace = new NebulaSpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpaceModel)tempSpace);
                    }
                    else if (randomNumber <= 2)
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
                    if (UnityEngine.Random.Range(1, 101) == 1)
                    {
                        pirates.Add(new PlatformAi(PirateModel.PirateType.Platform, this, link, tempSpace));
                    }

                    if (UnityEngine.Random.Range(1, 101) == 1)
                    {
                        tempSpace = new SpaceModel(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateStationView((StationModel)tempSpace);
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

    public void EndTurn()
    {
        foreach(PirateAiModel pirate in pirates)
        {
            pirate.EndTurn();
        }
    }

    // Gets spaces by coordinate. Avoid this method
    public SpaceModel GetSpace(int row, int column)
    {
        try
        {
            return map[row][column];
        }
        catch(IndexOutOfRangeException e)
        {
            return null;
        }
    }

    // Gives the map. Avoid this method
    public SpaceModel[][] getMap()
    {
        return map;
    }

    // Movement Methods. Only used in Space generation.
    public SpaceModel GetNE(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row - 1;
        int newSpaceColumn = startSpace.Column + 1;
        if(newSpaceRow < 0 || newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public SpaceModel GetE(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column + 2;
        if (newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public SpaceModel GetSE(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column + 1;
        if (newSpaceRow >= rows  || newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public SpaceModel GetSW(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column - 1;
        if (newSpaceRow >= rows || newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public SpaceModel GetW(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column - 2;
        if (newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public SpaceModel GetNW(SpaceModel startSpace)
    {
        int newSpaceRow = startSpace.Row - 1;
        int newSpaceColumn = startSpace.Column - 1;
        if (newSpaceRow < 0 || newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
}
