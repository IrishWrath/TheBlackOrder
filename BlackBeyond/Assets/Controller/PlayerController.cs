using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

// Class for Player only methods. 
public class PlayerController : MonoBehaviour
{
    private Text movementText;
    private PlayerModel playerModel;
    private GameObject shipView;
	private SoundController soundController;

    public void SetModel(PlayerModel model)
    {
        this.playerModel = model;
    }

    public void SetMovementTextInterface(Text movementText)
    {
        this.movementText = movementText;
    }

    public void Update()
    {

    }

    // Moves the ship to a new location
    // TODO make smoother with the update function
    public void MoveShip(SpaceModel destination)
    {
        // get vector2 of playerShip and assign to currentLocation
        Vector2 currentLocation = shipView.transform.position;
        Vector2 currentDestination = destination.GetController().GetPosition();

        // move playerShip gameobject to vector2 of destination from currentLocation
        shipView.gameObject.transform.position = Vector2.Lerp(currentLocation, currentDestination, 1);
		
		//play sound
		this.soundController.PlaySound(SoundController.Sound.move);
    }

    public void SetCurrentMovement(int currentPlayerMovement, int maxPlayerMovement)
    {
        movementText.text = currentPlayerMovement + "/" + maxPlayerMovement;
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }
	// For getting the soundController that can be found in the GameController
	public void setSoundController(SoundController soundController){
		this.soundController = soundController;
	}
}