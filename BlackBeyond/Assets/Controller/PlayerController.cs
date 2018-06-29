using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        // If left mouse button pressed perform raycast
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If raycast collided with object with SpaceHex tag
            if ((Physics.Raycast(ray, out hit)) && hit.transform.tag == "SpaceHex")
            {                
                // Assign space location of SpaceHex to destination
                SpaceModel destination = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();

                GetValidSpaces(destination);                                         
            }
        }
    }

    // UNDER CONSTRUCTION! Something is broken here. (Suspect issue might actually be in Pathfinding script as does not seem to allow multiple runs with different playerLocation)
    public void GetValidSpaces(SpaceModel destination)
    {
        int nodeCount = 0;
        Debug.Log("[" + playerModel.playerLocation.Row + ":" + playerModel.playerLocation.Column +"] Running Pathfinding..");
        
        // Get all spaces that are valid moves and return into list
        List<PathfindingNode> validMovementSpaces = DijkstrasPathfinding.GetSpacesForMovement(playerModel.playerLocation, playerModel.maxPlayerMovement);

        foreach (PathfindingNode node in validMovementSpaces)
            nodeCount++;

        Debug.Log("Pathfinding completed with " + nodeCount.ToString() + " results.");

        foreach (PathfindingNode node in validMovementSpaces)
        {
            SpaceModel space = node.GetSpace();

            Debug.Log("Space Co-ords: " + space.Row + ":" + space.Column);

            if (space == destination)
                MoveShip(destination);
        }
    }

    // Moves the ship to a new location
    // TODO make smoother with the update function
    public void MoveShip(SpaceModel destination)
    {
        if(playerModel.GetCurrentPlayerMovement() != 0)
        {
            // TEMPORARY MOVEMENT COST (Needs to be fed back from pathfinder)
            int movementCost = 1;

            // get vector2 of playerShip and assign to currentLocation
            Vector2 currentLocation = shipView.transform.position;
            Vector2 currentDestination = destination.GetController().GetPosition();

            // move playerShip gameobject to vector2 of destination from currentLocation
            shipView.gameObject.transform.position = Vector2.Lerp(currentLocation, currentDestination, 1);
            playerModel.UpdatePlayerLocation(destination);
            // pass cost of movement to playerModel
            //playerModel.UpdateCurrentPlayerMovement(movementCost);
        }        
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }
}