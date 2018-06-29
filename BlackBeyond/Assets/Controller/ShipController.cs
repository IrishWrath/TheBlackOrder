using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    public SpaceModel CurrentSpaceModel { get; private set; }

    private GameObject shipView;

    // Gives the GameObject
    public GameObject GetShipView()
    {
        return shipView;
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }

    // Moves the ship to a new location
    // TODO make smoother with the update function
    public void MoveShip(SpaceModel destination)
    {
        // GET PLAYER.CURRENTMOVEMENT

        // get vector2 of playerShip and assign to currentLocation
        Vector2 currentLocation = shipView.transform.position;
        Vector2 currentDestination = destination.GetController().GetPosition();

        // move playerShip gameobject to vector2 of destination from currentLocation
        shipView.gameObject.transform.position = Vector2.Lerp(currentLocation, currentDestination, 1);

        // UPDATE PLAYER.CURRENTMOVEMENT
    }
}
