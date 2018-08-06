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

    public void SetModel(PirateModel pirateModel, SoundController soundController)
    {
        this.pirateModel = pirateModel;
        // The superclass sometimes needs access to the model
        base.SetModel(pirateModel, soundController);
    }

    public void Die()
    {
        Destroy(shipView);
    }

    public void SetSprite(PirateModel.PirateType type)
    {
        switch(type)
        {
            case PirateModel.PirateType.Scout:
                shipView.GetComponentInChildren<SpriteRenderer>().sprite = ScoutSprite;
                break;
            case PirateModel.PirateType.Platform:
                shipView.GetComponentInChildren<SpriteRenderer>().sprite = PlatformSprite;
                break;
            case PirateModel.PirateType.Frigate:
                shipView.GetComponentInChildren<SpriteRenderer>().sprite = FrigateSprite;
                break;
            case PirateModel.PirateType.Destroyer:
                shipView.GetComponentInChildren<SpriteRenderer>().sprite = DestroyerSprite;
                break;
            case PirateModel.PirateType.Dreadnought:
                shipView.GetComponentInChildren<SpriteRenderer>().sprite = DreadnoughtSprite;
                break;
        }
    }

}