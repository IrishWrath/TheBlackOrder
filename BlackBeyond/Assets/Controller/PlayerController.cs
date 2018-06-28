using UnityEngine;
using System.Collections;
using System;

// Class for Player only methods. 
public class PlayerController : ShipController
{
    private Player model;

    public void SetModel(Player model)
    {
        this.model = model;
    }

    public void Update()
    {
        // Player Movement Logic should be here.
    }
}
