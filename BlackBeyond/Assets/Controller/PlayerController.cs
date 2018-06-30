using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

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
        // If left mouse button pressed and mouse not over the UI
        if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject()))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // If raycast collided with object with SpaceHex tag
            if ((Physics.Raycast(ray, out hit)) && (hit.transform.tag == "SpaceHex"))
            {
                // Assign space location of SpaceHex to destination
                SpaceModel destination = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();

                GetValidSpaces(destination);
            }
        }
    }

    // UNDER CONSTRUCTION! 
    public void GetValidSpaces(SpaceModel destination)
    {
        // Get all spaces that are valid moves and return into list
        List<PathfindingNode> validMovementSpaces = DijkstrasPathfinding.GetSpacesForMovement(playerModel.playerLocation, playerModel.currentPlayerMovement);

        // go through each node in pathfinding list and get the space location
        foreach (PathfindingNode node in validMovementSpaces)
        {
            SpaceModel space = node.GetSpace();

            // if destination is one of the node space locations
            if (space == destination)
            {
                MoveShip(destination, node.GetCost());
            }                
        }
    }

    // Moves the ship to a new location
    // TODO make smoother with the update function
    public void MoveShip(SpaceModel destination, int movementCost)
    {
        if((playerModel.GetCurrentPlayerMovement() - movementCost) >= 0)
        {
            // get vector2 of playerShip and assign to currentLocation
            Vector2 currentLocation = shipView.transform.position;
            Vector2 currentDestination = destination.GetController().GetPosition();

            // move playerShip gameobject to vector2 of destination from currentLocation
            shipView.gameObject.transform.position = Vector2.Lerp(currentLocation, currentDestination, 1);

            // update location to model after move
            playerModel.UpdatePlayerLocation(destination);
            
            // pass cost of movement to playerModel
            playerModel.UpdateCurrentPlayerMovement(movementCost);
            Debug.Log("Moves Available: " + playerModel.currentPlayerMovement.ToString());
        }        
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }
}