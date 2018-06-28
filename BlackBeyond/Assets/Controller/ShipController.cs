using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    public Space CurrentSpace { get; private set; }

    private GameObject ship;

    // Gives the GameObject
    public GameObject GetShipView()
    {
        return ship;
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject ship)
    {
        this.ship = ship;
    }

    // Moves the ship to a new location
    // TODO make smoother with the update function
    public void MoveShip(Space destination)
    {
        Vector2 currentLocation = ship.transform.position;        // get vector2 of playerShip and assign to currentLocation
        ship.gameObject.transform.position = Vector2.Lerp(currentLocation, destination.GetController().GetPosition(), 1);     // move playerShip gameobject to vector2 of destination from currentLocation
    }
}
