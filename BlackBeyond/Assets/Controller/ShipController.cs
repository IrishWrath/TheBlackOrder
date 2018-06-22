using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Space CurrentSpace { get; private set; }
    private readonly GameObject ship;       // Is this still required? ***
    GameObject playerShip = GameObject.Find("PlayerShip(Clone)");       // create and assign gameobject variable playerShip

    //public static ShipController Create(Space startSpace, GameObject toSpawn)
    //{
    //    ShipController ship = CreateInstance<ShipController>();
    //    ship.CurrentSpace = startSpace;
    //    ship.ship = Instantiate(toSpawn, startSpace.GetPosition(), Quaternion.identity) as GameObject;
    //    return ship;
    //}

    public void Move(Space newSpace)        //Is this function still required? ***
    {
        CurrentSpace = newSpace;
        ship.gameObject.transform.position = newSpace.GetCallback().GetPosition();
    }

    public void moveShip(Space destination)
    {
        Vector2 currentLocation = playerShip.transform.position;        // get vector2 of playerShip and assign to currentLocation
        playerShip.gameObject.transform.position = Vector2.MoveTowards(currentLocation, destination.GetCallback().GetPosition(), 5.0f);     // move playerShip gameobject to vector2 of destination from currentLocation
    }
}
