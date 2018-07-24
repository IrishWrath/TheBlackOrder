using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : ShipController
{
    public bool engaged;
    private PirateModel pirateModel;


    public void SetModel(PirateModel pirateModel)
    {
        this.pirateModel = pirateModel;
        // The superclass sometimes needs access to the model
        base.SetModel(pirateModel);
    }
}