using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Class, possibly unnecessary.
public class MapController 
{
    // Creates Map 
    public MapController(int rows, int columns, ModelLink link, StationModel stationModel)
    {
        this.Map = new MapModel(rows, columns, link, stationModel);
    }

    // The Map Model
    public MapModel Map { get; private set; }
}
