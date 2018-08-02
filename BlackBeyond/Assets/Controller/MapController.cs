using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Class, possibly unnecessary.
public class MapController 
{
    // Creates Map 
    public MapController(int rows, int columns, ModelLink link, GameController gameController)
    {
        this.Map = new ProceduralMapModel(rows, columns, link, gameController);
    }

    // The Map Model
    public MapModel Map { get; private set; }
}
