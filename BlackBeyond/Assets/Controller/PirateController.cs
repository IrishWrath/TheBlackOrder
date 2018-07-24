using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : ShipController
{
    public bool engaged;
    private PirateModel pirateModel;
    private GameObject pirateView;

    // Don't need AI logic here
    //public void Engagement()
    //{
    //    // Oisín has changed this to get the movement of this pirate ship (fixing errors so unity could add code to components)
    //    while (pirateModel.GetCurrentMovement() != 0)
    //    {
    //        if (engaged == true)
    //        {
    //            //pirateShip scans spaces inside detection range
    //            //is target within detection range, if yes
    //            //is target within attackRange, attack, reduce movment counter by 1

    //            int armor = PlayerModel.GetArmor();
    //            int currentHealth = PlayerModel.GetHealth();
    //            int shotDamage = pirateModel.GetDamage();
    //            int adjDamage = armor - shotDamage;
    //            if (adjDamage <=0)
    //            {
    //                adjDamage = 0;
    //            }
    //            int remainingHP = currentHealth - adjDamage;
    //            PlayerModel.UpdatePlayerHealth(remainingHP);




    //        }
    //        else
    //        {
    //            engaged = false;
    //            //follow patrol route for 1 movement
    //            //pirateShip scans spaces inside detection range
    //            //if target found engaged = true

    //        }
    //    }


    //}

    public void SetShipView(GameObject pirateView)
    {
        this.pirateView = pirateView;
    }

    public void SetModel(PirateModel pirateModel)
    {
        this.pirateModel = pirateModel;
    }
}