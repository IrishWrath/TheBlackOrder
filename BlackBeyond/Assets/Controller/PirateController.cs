using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public bool engaged;
    private PirateModel pirateModel;
    

    public void Engagement()
    {
        // Oisín has changed this to get the movement of this pirate ship (fixing errors so unity could add code to components)
        while (pirateModel.GetCurrentMovement() != 0)
        {
            if (engaged == true)
            {
                //pirateShip scans spaces inside detection range
                //is target within detection range, if yes
                //is target within attackRange, attack, reduce movment counter by 1

                int armor = PlayerModel.GetArmor();
                int currentHealth = PlayerModel.GetHealth();
                int shotDamage = pirateModel.GetDamage();
                int adjDamage = armor - shotDamage;
                if (adjDamage <=0)
                {
                    adjDamage = 0;
                }
                int remainingHP = currentHealth - adjDamage;
                PlayerModel.UpdatePlayerHealth(remainingHP);
               
                


            }
            else
            {
                engaged = false;
                //follow patrol route for 1 movement
                //pirateShip scans spaces inside detection range
                //if target found engaged = true
                
            }
        }

        
    }

    public void SetShipView(GameObject pirateView)
    {
        throw new NotImplementedException();
    }

    public void SetModel(PirateModel pirateModel)
    {
        this.pirateModel = pirateModel;
    }

    public void EndOfTurn()
    {
        /*END OF TURN METHOD(will be called at end of turn by the turn structure)

In PatrolPath Model class
CONSTRUCTOR
gets the patrol path from pathfinding
END OF TURN */


//If the pirate is dead check name and create a new one
    int pirateHealth = PirateModel.GetHealth();
    if (pirateHealth <= 0)
        {
            String name = PirateModel.GetName();
            if (name.Equals("Scout"))
            {
                PirateModel.CreateScoutPirate();
            } else if (name.Equals("Frigate"))
            {
                PirateModel.CreateFrigatePirate();
            }
            else if (name.Equals("Platform"))
            {
                PirateModel.CreatePlatformPirate();
            }
            else if (name.Equals("Destroyer"))
            {
                PirateModel.CreateDestroyerPirate();
            }
            else 
            {
                PirateModel.CreateDreadnaughtPirate();
            }
        }
    }
}