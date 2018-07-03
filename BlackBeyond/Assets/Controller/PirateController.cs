using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public bool engaged;
    public void Engagement()
    {
        while (PirateModel.frigate.GetCurrentMovement != 0)
        {
            if (engaged == true)
            {
                //pirateShip scans spaces inside detection range
                //is target within detection range, if yes
                //is target within attackRange, attack, reduce movment counter by 1

                /*need to add player health, armor, shot damage to PlayerModel Class*/

                //PlayerHealth = PlayerHealth - (PirateModel.frigate.shotDamage-playerArmor) 
                


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
    /*END OF TURN METHOD(will be called at end of turn by the turn structure)

    In PatrolPath Model class
CONSTRUCTOR
    gets the patrol path from pathfinding
END OF TURN
    If the pirate is dead
        make a new one*/
}