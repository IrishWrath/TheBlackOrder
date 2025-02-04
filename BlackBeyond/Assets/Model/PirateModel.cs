﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateModel : ShipModel
{
    // This is like the name being used previously, but an enum is generally easier to use
    public enum PirateType
    {
        Platform, Scout, Frigate, Destroyer, Dreadnought
    }

    // Most of the stats have been moved up to the ShipModel, the superclass
    private int playerDetectRange;
    private PirateType type;

    private PirateController pirateController;
    private PirateAiModel pirateAi;


    //pirate ship builder template
    public PirateModel(SpaceModel location, PirateType type, int health, int shotDamage, int detectRange, int attackRange, int maxPirateMovement , int shotCounter, PirateAiModel pirateAi)
    {
        // The "base" for these are unnecessary, but it helps keep them seperate. Base gets the superclass
        base.currentSpace = location;
        this.type = type;
        base.shipHealth = health;
        base.maxHealth = health;
        base.shotDamage = shotDamage;
        this.playerDetectRange = detectRange;
        base.attackRange = attackRange;
        base.maxMovement = maxPirateMovement;
        base.currentMovement = maxPirateMovement;
        base.shotCounter = shotCounter;
        base.currentShotCounter = shotCounter;
        this.pirateAi = pirateAi;

        base.shipArmor = 0;
    }

    public int GetDetectRange()
    {
        return playerDetectRange;
    }
    public int GetMaxMovement()
    {
        return maxMovement;
    }

    public void UpdatePirateLocation(SpaceModel location)
    {
        base.currentSpace.LeaveSpace();
        base.currentSpace = location;
        location.OccupySpace(this);
    }

    public PirateController GetController()
    {
        return pirateController;
    }

    public void SetController(PirateController controller)
    {
        this.pirateController = controller;
        base.SetController(controller);
        pirateController.SetSprite(type);
    }

    public override void Die()
    {
        FinishedAnimatingMovement();
        pirateAi.NullPirate();
        currentSpace.LeaveSpace();
        pirateController.Die();
    }

	public override void FinishedAnimatingMovement()
	{
        base.FinishedAnimatingMovement();
        pirateAi.FinishedMovement();
	}

	// Static methods for creating pirates
	public static PirateModel CreateScoutPirate(SpaceModel location, PirateAiModel pirateAiModel)
    {
        return new PirateModel(location, PirateType.Scout, 2, 2, 3, 1, 4, 2, pirateAiModel);
    }
    public static PirateModel CreateFrigatePirate(SpaceModel location, PirateAiModel pirateAiModel)
    {
        return new PirateModel(location, PirateType.Frigate, 4, 3, 3, 2, 3, 1, pirateAiModel);
    }
    public static PirateModel CreatePlatformPirate(SpaceModel location, PirateAiModel pirateAiModel)
    {
        return new PirateModel(location, PirateType.Platform, 4, 2, 5, 5, 0, 2, pirateAiModel);
    }
    public static PirateModel CreateDestroyerPirate(SpaceModel location, PirateAiModel pirateAiModel)
    {
        return new PirateModel(location, PirateType.Destroyer, 7, 4, 3, 3, 2, 1, pirateAiModel);
    }
    public static PirateModel CreateDreadnaughtPirate(SpaceModel location, PirateAiModel pirateAiModel)
    {
        return new PirateModel(location, PirateType.Dreadnought, 10, 5, 2, 3, 2, 1, pirateAiModel);
    }
    // End pirate creation methods
}
