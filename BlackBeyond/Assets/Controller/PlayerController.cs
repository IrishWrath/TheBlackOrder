using UnityEngine;
using System.Collections;
using System;

public class PlayerController : ShipController, IPlayerCallback
{
    private Player model;

    public void SetModel(Player model)
    {
        this.model = model;
    }
}
