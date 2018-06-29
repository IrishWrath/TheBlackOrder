using UnityEngine;
using System.Collections;
using System;

// Class for Player only methods. 
public class PlayerController : ShipController
{
    private PlayerModel playerModel;

    public void SetModel(PlayerModel model)
    {
        this.playerModel = model;
    }

    public void Update()
    {

    }
}