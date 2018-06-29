using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }

    // A model link. TODO This class might be better as a static class
    private ModelLink modelLink;

    // The Prefab for Spaces
    public GameObject spaceView;
    // The Prefab for Player's ship
    public GameObject playership;
    // The Nebula Terrain
    public GameObject nebulaTerrain;

    // Container for spaces
    public GameObject mapGameObject;

    // A reference to the player.
    private PlayerModel playerModel;

    // Use this for initialization. Starting method for our code.
    public void Start()
    {
        this.modelLink = new ModelLink(this, mapGameObject);

        // Creates the map.
        this.MapController = new MapController(125, 250, modelLink);

        // Gets a starting space for the player, based on coordinates. TODO moving away from coordinates, find another method of getting spaces
        SpaceModel playerSpace = MapController.Map.GetSpace(62, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace);
        modelLink.CreatePlayerView(playerModel);
    }

    // Returns the Prefabs
    public GameObject GetSpaceView()
    {
        return spaceView;
    }
    public GameObject GetPlayerView()
    {
        return playership;
    }
    public GameObject GetNebula()
    {
        return nebulaTerrain;
    }


    // This Update should be avoided. Only place testing code here.
    // Update is called once per frame
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
                ////Testing A*
                //foreach(SpaceModel space in AStarPathfinding.GetPathToDestination(playerModel.GetSpace(), hit.transform.gameObject.GetComponent<SpaceController>().GetSpace()))
                //{
                //    space.GetController().TestAstroids();
                //}


                //// Create required instance of ShipController class
                //PlayerController playerController = playerModel.GetController();

                //// Assign space location of SpaceHex to destination
                //SpaceModel destination = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();

                //// Call moveShip function in shipcontroller and pass destination space
                //playerController.MoveShip(destination);       
                


                // Oisín: I think it would be best to call a move method in ShipController (Not my one though. That doesn't work yet.)
                //ship.gameObject.transform.position = newSpace.GetCallback().GetPosition();

//                 This is a test method
//                 List<PathfindingNode> nodes =  new DijkstrasPathfinding(newSpace, 1).GetNodes();
//                 foreach(PathfindingNode node in nodes)
//                 {
//                     node.GetSpace().GetCallback().SetSelectable(node.GetCost());
//                 }

                //Tell the model to move instead
                //player.Move();
                //  in that method, callback.move()
                //      view (the gameobject) <- set position.
            }
        }
    }
}
