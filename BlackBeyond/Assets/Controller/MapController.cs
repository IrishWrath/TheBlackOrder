using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Class, possibly unnecessary.
public class MapController 
{
    // Creates Map 
    public MapController(int rows, int columns, ModelLink link)
    {
        this.Map = new Map(rows, columns, link);
    }

    // The Map Model
    public Map Map { get; private set; }
}
