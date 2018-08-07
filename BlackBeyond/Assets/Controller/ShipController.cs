using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityToolbag;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    //public SpaceModel CurrentSpaceModel { get; private set; }

    public GameObject laserPrefab;
    public GameObject slider;

    // shipview is the ships gameobject
    protected GameObject shipView;
    protected ShipModel shipModel;
    // is this ship moving
    private bool moving = false;

    //destinations
    private List<SpaceModel> destinations;
    private PirateModel pirateMoving;
    private PlayerModel playerToShootOnFinish;
    private float distanceMoved = 0f;
    private float speed = 2f;

    Vector2 currentLocation, currentDestination;
    private int destinationIndex;

	public void Start()
	{
        slider.GetComponent<Slider>().maxValue = 1;
        slider.GetComponent<Slider>().value = 1;
	}

	public void SetModel(ShipModel shipModel, SoundController soundController)
    {
        this.shipModel = shipModel;
        this.shipModel.SetSoundController(soundController);
    }

    // Gives the GameObject
    public GameObject GetShipView()
    {
        return shipView;
    }

    public void UpdateHealth(int shipHealth, int maxHealth)
    {
        Dispatcher.InvokeAsync(() =>
        {
            slider.GetComponent<Slider>().maxValue = maxHealth;
            slider.GetComponent<Slider>().value = shipHealth;
        });
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
        this.destinations = new List<SpaceModel>();
        foreach(PathfindingNode node in destinations)
        {
            this.destinations.Add(node.GetSpace());
        }

        distanceMoved = 0;
        destinationIndex = 1;

        currentLocation = shipView.transform.position;
        currentDestination = destinations[destinationIndex].GetSpace().GetController().GetPosition();
        FlipShip(currentLocation.x < currentDestination.x);
        //not sure if getting this back from the ship model makes sense, but it does work. The ShipController could just have
        //a sound controller too.
        Dispatcher.InvokeAsync(() =>
        {
            shipModel.soundController.PlaySound(SoundController.Sound.move);
        });
    }

    public void MoveShip(List<SpaceModel> destinations, PirateModel pirateMoving, PlayerModel playerToShootOnFinish)
    {
        moving = true;
        this.destinations = destinations;
        this.pirateMoving = pirateMoving;
        this.playerToShootOnFinish = playerToShootOnFinish;
        distanceMoved = 0;
        destinationIndex = 0;

        currentLocation = shipView.transform.position;
        if (destinations.Count != 0)
        {
            currentDestination = destinations[destinationIndex].GetController().GetPosition();
            FlipShip(currentLocation.x < currentDestination.x);
        }
        else
        {
            currentDestination = new Vector2(-9999, -9999);
        }
    }

	private void Update()
	{
        if (moving && currentDestination.x > -9999)
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
                if (destinationIndex >= destinations.Count)
                {
                    moving = false;
                    shipModel.FinishedAnimatingMovement();
                    if (pirateMoving != null)
                    {
                        pirateMoving.FinishedAnimatingMovement();
                    }
                    if (playerToShootOnFinish != null)
                    {
                        pirateMoving.Shoot(playerToShootOnFinish);
                        pirateMoving = null;
                        playerToShootOnFinish = null;
                    }
                }
                else
                {
                    currentLocation = currentDestination;
                    currentDestination = destinations[destinationIndex].GetController().GetPosition();
                    FlipShip(currentLocation.x < currentDestination.x);
                }
            }
        }
        else if(!(currentDestination.x > -9999))
        {
            if (pirateMoving != null)
            {
                pirateMoving.FinishedAnimatingMovement();
            }
            if (playerToShootOnFinish != null)
            {
                pirateMoving.Shoot(playerToShootOnFinish);
                shipModel.soundController.SwitchMusic(SoundController.Sound.battle);
                pirateMoving = null;
                playerToShootOnFinish = null;
            }
        }
	}

    public void CreateLaser(SpaceModel start, SpaceModel end)
    {
        Dispatcher.InvokeAsync(() =>
        {
            var laser = UnityEngine.Object.Instantiate(laserPrefab) as GameObject;
            laser.GetComponent<Laser>().SetLine(start.GetController().GetPosition(), end.GetController().GetPosition());
        });
    }

    public void FlipShip(bool turnRight)
    {
        Dispatcher.InvokeAsync(() =>
        {
            Vector3 newScale = gameObject.transform.localScale;
            if (turnRight)
            {

                newScale.x = Mathf.Abs(newScale.x);

            }
            else
            {
                newScale.x = -Mathf.Abs(newScale.x);
            }
            gameObject.transform.localScale = newScale;
        });
    }
}
