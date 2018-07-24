using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    //public SpaceModel CurrentSpaceModel { get; private set; }

    public GameObject laserPrefab;

    // shipview is the ships gameobject
    protected GameObject shipView;
    protected ShipModel shipModel;
    // is this ship moving
    private bool moving = false;

    //destinations
    private PathfindingNode[] destinations;

    private float distanceMoved = 0f;
    private float speed = 2f;

    Vector2 currentLocation, currentDestination;
    private int destinationIndex;


    public void SetModel(ShipModel shipModel)
    {
        this.shipModel = shipModel;
    }

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
    // made smoother with the update function
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
                    shipModel.FinishedAnimatingMovement();
                }
                else
                {
                    currentLocation = currentDestination;
                    currentDestination = destinations[destinationIndex].GetSpace().GetController().GetPosition();
                }
            }
        }
	}

    public void CreateLaser(SpaceModel start, SpaceModel end)
    {
        var laser = Object.Instantiate(laserPrefab) as GameObject;
        laser.GetComponent<Laser>().SetLine(start.GetController().GetPosition(), end.GetController().GetPosition());
    }
}
