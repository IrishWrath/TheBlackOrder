using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Class, possibly unnecessary.
public class MapController 
{
    // Creates Map 
    public MapController(int rows, int columns, IModelLink link)
    {
        this.Map = new Map(rows, columns, link);
    }

    // The Map Model
    public Map Map { get; private set; }

    // Not Required, can just call the property above
    internal Space GetSpace(int row, int column)
    {
        return Map.GetSpace(row, column);
    }
}
