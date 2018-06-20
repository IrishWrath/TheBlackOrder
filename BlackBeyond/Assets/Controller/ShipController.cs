using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
    public Space CurrentSpace { get; private set; }
    private readonly GameObject ship;

    //public static ShipController Create(Space startSpace, GameObject toSpawn)
    //{
    //    ShipController ship = CreateInstance<ShipController>();
    //    ship.CurrentSpace = startSpace;
    //    ship.ship = Instantiate(toSpawn, startSpace.GetPosition(), Quaternion.identity) as GameObject;
    //    return ship;
    //}

    public void Move(Space newSpace)
    {
        CurrentSpace = newSpace;
        ship.gameObject.transform.position = newSpace.GetCallback().GetPosition();
    }


}
