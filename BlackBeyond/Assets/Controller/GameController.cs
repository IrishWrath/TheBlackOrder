using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }

    // A model link. TODO This class might be better as a static class
    private IModelLink modelLink;

    // The Prefab for Spaces
    public GameObject spaceView;
    // The Prefab for Player's ship
    public GameObject playership;

    // A reference to the player.
    private Player player;

    // Use this for initialization. Starting method for our code.
    public void Start()
    {
        this.modelLink = new ModelLink(this);

        // Creates the map.
        this.MapController = new MapController(5, 5, modelLink);

        // Gets a starting space for the player, based on coordinates. TODO moving away from coordinates, find another method of getting spaces
        Space playerSpace = MapController.GetSpace(1, 4);

        // Create a player, and set up MVC connections
        this.player = new Player(playerSpace);
        modelLink.CreatePlayerView(player);
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


    // This Update should be avoided. Only place testing code here.
    // Update is called once per frame
    void Update()
    {
        // If left mouse button pressed perform raycast
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))      // If raycast collided with object
            {
                if (hit.transform.tag == "SpaceHex")        // If object collided was SpaceHex
                {

                    PlayerController ShC = (PlayerController)player.GetCallback();       // Create required instance of ShipController class



                    Space destination = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();        // Convert vector2 location of spacehex to a space and then assign to destination
                    
                    //Debug.Log("Destination Co-ords: " + destination.Row + ":" + destination.Column);
                    //Debug.Log("Destination Vector2: " + destination.GetCallback().GetPosition().ToString());

                    ShC.MoveShip(destination);       // call moveShip function in shipcontroller and pass destination space




                    // Oisín: I think it would be best to call a move method in ShipController (Not my one though. That doesn't work yet.)
                    //ship.gameObject.transform.position = newSpace.GetCallback().GetPosition();

//                     This is a test method
//                     List<PathfindingNode> nodes =  new DijkstrasPathfinding(newSpace, 1).GetNodes();
//                     foreach(PathfindingNode node in nodes)
//                     {
//                         node.GetSpace().GetCallback().SetSelectable(node.GetCost());
//                     }

                    //Tell the model to move instead
                    //player.Move();
                    //  in that method, callback.move()
                    //      view (the gameobject) <- set position.
                }
            }
        }
    }
}
