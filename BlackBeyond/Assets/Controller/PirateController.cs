using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour {
    public bool engaged;
    public void engagement()
    {
    if (engaged = true)
    {
            //pirateShip scans spaces inside detection range
            //is target within detection range, if yes
            //is target within attackRange, attack, reduce movment counter by 1
            //else move closer and repeat
            //

        }


    }



/*END OF TURN METHOD(will be called at end of turn by the turn structure)
  bool engaged;
  
 
    If pursuing,
        Pursue
    else
        repeat
            check for enemies(calling a range function)
                shoot them
                start pursuing
            move to next space on path
In PatrolPath Model class
CONSTRUCTOR
    gets the patrol path from pathfinding
END OF TURN
    If the pirate is dead
        make a new one*/
