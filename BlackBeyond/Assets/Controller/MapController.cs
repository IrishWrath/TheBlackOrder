using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController 
{
    public MapController(int rows, int columns, IModelLink link)
    {
        this.Map = new Map(rows, columns, link);
    }

    public Map Map { get; private set; }

    internal Space GetSpace(int row, int column)
    {
        return Map.GetSpace(row, column);
    }
}
