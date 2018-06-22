using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Space CurrentSpace { get; private set; }
    private GameObject ship;       // Is this still required? ***


    //GameObject playerShip = GameObject.Find("PlayerShip(Clone)");       // create and assign gameobject variable playerShip

    //public static ShipController Create(Space startSpace, GameObject toSpawn)
    //{
    //    ShipController ship = CreateInstance<ShipController>();
    //    ship.CurrentSpace = startSpace;
    //    ship.ship = Instantiate(toSpawn, startSpace.GetPosition(), Quaternion.identity) as GameObject;
    //    return ship;
    //}

    public GameObject GetShipView()
    {
        return ship;
    }

    public void SetShipView(GameObject ship)
    {
        this.ship = ship;
    }

    public void MoveShip(Space destination)
    {
        Vector2 currentLocation = ship.transform.position;        // get vector2 of playerShip and assign to currentLocation
        ship.gameObject.transform.position = Vector2.MoveTowards(currentLocation, destination.GetCallback().GetPosition(), 6.0f);     // move playerShip gameobject to vector2 of destination from currentLocation
    }
}
