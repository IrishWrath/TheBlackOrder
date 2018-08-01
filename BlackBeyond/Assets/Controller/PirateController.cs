using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : ShipController
{
    public bool engaged;
    private PirateModel pirateModel;

    public Sprite ScoutSprite;
    public Sprite PlatformSprite;
    public Sprite FrigateSprite;
    public Sprite DestroyerSprite;
    public Sprite DreadnoughtSprite;

    public void SetModel(PirateModel pirateModel)
    {
        this.pirateModel = pirateModel;
        // The superclass sometimes needs access to the model
        base.SetModel(pirateModel);
    }

    public void SetSprite(PirateModel.PirateType type)
    {
        switch(type)
        {
            case PirateModel.PirateType.Scout:
                shipView.GetComponent<SpriteRenderer>().sprite = ScoutSprite;
                break;
            case PirateModel.PirateType.Platform:
                shipView.GetComponent<SpriteRenderer>().sprite = PlatformSprite;
                break;
            case PirateModel.PirateType.Frigate:
                shipView.GetComponent<SpriteRenderer>().sprite = FrigateSprite;
                break;
            case PirateModel.PirateType.Destroyer:
                shipView.GetComponent<SpriteRenderer>().sprite = DestroyerSprite;
                break;
            case PirateModel.PirateType.Dreadnought:
                shipView.GetComponent<SpriteRenderer>().sprite = DreadnoughtSprite;
                break;
        }
    }
}