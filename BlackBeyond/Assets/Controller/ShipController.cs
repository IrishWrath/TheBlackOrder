using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    public SpaceModel CurrentSpaceModel { get; private set; }

    // shipview is the ships gameobject
    protected GameObject shipView;

    // is this ship moving
    private bool moving = false;

    //destinations
    private PathfindingNode[] destinations;

    private float distanceMoved = 0f;
    private float speed = 1.5f;

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
    public void MoveShip(PathfindingNode[] destinations)
    {

        moving = true;
        this.destinations = destinations;

        distanceMoved = 0;
        destinationIndex = 1;

        currentLocation = shipView.transform.position;
        currentDestination = destinations[destinationIndex].GetSpace().GetController().GetPosition();

    }

	private void Update()
	{
		if (moving)
        {
            // add some distance
            distanceMoved += speed * Time.deltaTime;

            //move along according to speed;
            shipView.transform.position = Vector2.Lerp(currentLocation, currentDestination, distanceMoved);

            //check if we've reached the goal point
            if (distanceMoved >= 1)
            {
                distanceMoved = 0;
                destinationIndex += 1;
                // check if we're done moving
                if (destinationIndex >= destinations.Length)
                {
                    moving = false;
                }
                else
                {
                    currentLocation = currentDestination;
                    currentDestination = destinations[destinationIndex].GetSpace().GetController().GetPosition();
                }
            }
        }
	}
}
