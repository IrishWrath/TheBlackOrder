using UnityEngine;
using System.Collections;
using System;

// Class for Player only methods. 
public class PlayerController : ShipController
{
    private PlayerModel model;

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    public void Update()
    {

    }
}