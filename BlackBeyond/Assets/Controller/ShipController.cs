using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    public SpaceModel CurrentSpaceModel { get; private set; }

    // shipview is the ships gameobject
    private GameObject shipView;

    // is this ship moving
    private bool moving = false;

    //destinations
    private SpaceModel[] destinations;

    private float distanceMoved = 0f;
    private float speed = 0.2f;

    Vector2 currentLocation, currentDestination;
    private int destinationIndex;


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
    // TODO, this controller should be renamed Pirate Controller, and do pirate things
    public void MoveShip(SpaceModel[] destinations)
    {

        moving = true;
        this.destinations = destinations;

        distanceMoved = 0;
        destinationIndex = 0;

        currentLocation = shipView.transform.position;
        currentDestination = destinations[destinationIndex].GetController().GetPosition();

    }

	private void Update()
	{
		if (moving)
        {

            // add some distance
            distanceMoved += speed * Time.deltaTime;

            //move along according to speed;
            currentLocation = Vector2.Lerp(currentLocation, currentDestination, distanceMoved);

            //set our transform to the new position
            shipView.transform.position = currentLocation;

            //check if we've reached the goal point
            if (distanceMoved >= 1)
            {
                distanceMoved = 0;
                destinationIndex += 1;

                currentLocation = currentDestination;
                currentDestination = destinations[destinationIndex].GetController().GetPosition();

                // check if we're done moving
                if (destinationIndex >= destinations.Length)
                {
                    moving = false;

                }
            }
        }
	}
}
