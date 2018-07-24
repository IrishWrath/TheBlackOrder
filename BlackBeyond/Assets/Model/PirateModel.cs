using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateModel 
{
    public enum PirateType
    {
        Platform, Scout, Frigate, Destroyer, Dreadnought
    }

    private PirateController pirateController;
    private SpaceModel pirateSpaceModel;

    //ship combat stat variables
    private string shipName;
    private int shipHealth;
    private int shotDamage;
    private int playerDetectRange;
    private int attackRange;
    public int maxPirateMovement;
    public int currentPirateMovement;
        
    //pirate ship builder template
    public PirateModel(SpaceModel location, string name, int health, int shotDamage, int detectRange, int attackRange , int maxPirateMovement, int currentPirateMovement)
    {
        this.pirateSpaceModel = location;
        shipName = name;
        shipHealth = health;
        this.shotDamage = shotDamage;
        playerDetectRange = detectRange;
        this.attackRange = attackRange;
        this.maxPirateMovement = maxPirateMovement;
        this.currentPirateMovement = currentPirateMovement;
    }

    public static PirateModel CreateScoutPirate(SpaceModel location)
    {
        return new PirateModel(location, "Scout", 2, 2, 3, 1, 4, 4);
    }

    public static PirateModel CreateFrigatePirate(SpaceModel location)
    {
        return new PirateModel(location, "Frigate", 4, 3, 3, 2, 3, 3);
    }

    public static PirateModel CreatePlatformPirate(SpaceModel location)
    {
        return new PirateModel(location, "Platform",4, 2, 5, 5, 0, 0);
    }

    public static PirateModel CreateDestroyerPirate(SpaceModel location)
    {
        return new PirateModel(location, "Destroyer", 7, 4, 3, 3, 2, 2);
    }

    public static PirateModel CreateDreadnaughtPirate(SpaceModel location)
    {
        return new PirateModel(location, "Dreadnaught", 10, 5, 2, 3, 2, 2);
    }

    public string GetName()
    {
        return shipName;
    }
    public int GetDamage()
    {
        return shotDamage;
    }

    public int GetHealth()
    {
        return shipHealth;
    }
    public void SetHealth(int newHealth)
    {
        shipHealth = newHealth;
    }

    public void Shoot(PlayerModel player)
    {
        int armor = player.GetArmor();
        int currentHealth = player.GetHealth();
        int adjDamage = armor - shotDamage;
        if (adjDamage <= 0)
        {
            adjDamage = 0;
        }
        int remainingHP = currentHealth - adjDamage;
        player.UpdatePlayerHealth(remainingHP);
    }

    public int GetDetectRange()
    {
        return playerDetectRange;
    }



    public PirateController GetController()
    {
        return pirateController;
    }

    public void SetController(PirateController controller)
    {
        this.pirateController = controller;
    }

    //public PirateModel(SpaceModel pirateSpace)
    //{
    //    pirateSpaceModel = pirateSpace;
    //}

    public SpaceModel GetSpace()
    {
        return pirateSpaceModel;
    }

    public int GetCurrentMovement()
    {
        return currentPirateMovement;
    }

    public void UpdateCurrentMovement(int movementUsed)
    {
        currentPirateMovement = currentPirateMovement - movementUsed;
    }
}
