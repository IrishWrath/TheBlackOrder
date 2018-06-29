using UnityEngine;
using System.Collections;
using System;

// Class for Player only methods. 
public class PlayerController : MonoBehaviour
{
    private PlayerModel playerModel;
    private GameObject shipView;

    public void SetModel(PlayerModel model)
    {
        this.playerModel = model;
    }

    public void Update()
    {

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

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }
}