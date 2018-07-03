using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour {

	// Use this for initialization
	void Start () {


    }

// Update is called once per frame
void Update () {
		
	}
}



/*END OF TURN METHOD(will be called at end of turn by the turn structure)
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
