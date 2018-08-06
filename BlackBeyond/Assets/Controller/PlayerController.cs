using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

// Class for Player only methods. 
public class PlayerController : ShipController
{
    private Text movementText;
    //private PlayerModel playerModel;

    //public void SetModel(PlayerModel model)
    //{
    //    this.playerModel = model;
    //}

    public void SetMovementTextInterface(Text movementText)
    {
        this.movementText = movementText;
    }

    public void SetCurrentMovement(int currentPlayerMovement, int maxPlayerMovement)
    {
        movementText.text = currentPlayerMovement + "/" + maxPlayerMovement;
    }

}