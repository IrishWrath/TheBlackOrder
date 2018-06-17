﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    private Space[][] map;
    private int rows;
    private int columns;

    public Map(int rows, int columns, ModelLink link)
    {
        this.rows = rows;
        this.columns = columns;
        map = new Space[rows][];
        for (int row = 0; row < rows; row++)
        {
            map[row] = new Space[columns*2];
            if(row%2==1)
            {
                for (int column = 0; column < columns * 2; column += 2)
                {
                    map[row][column] = new Space(row, column, link);
                }
            }
            else
            {
                for (int column = 1; column < columns * 2; column += 2)
                {
                    map[row][column] = new Space(row, column, link);
                }
            }
        }
    }

    public Space GetSpace(int row, int column)
    {
        return map[row][column];
    }

    public Space[][] getMap()
    {
        return map;
    }

    public Space moveNE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row - 1;
        int newSpaceColumn = startSpace.Column + 1;
        if(newSpaceRow < 0 || newSpaceColumn > columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public Space moveE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column + 2;
        if (newSpaceColumn > columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public Space moveSE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column + 1;
        if (newSpaceRow > rows  || newSpaceColumn > columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space moveSW(Space startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column - 1;
        if (newSpaceRow > rows || newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space moveW(Space startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column - 2;
        if (newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space moveNW(Space startSpace)
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