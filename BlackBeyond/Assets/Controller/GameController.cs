using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }

    private IModelLink modelLink;
    public GameObject spaceView;
    public GameObject playership;
    private Player player;

    // Use this for initialization
    public void Start()
    {
        this.modelLink = new ModelLink(this);
        this.MapController = new MapController(5, 5, modelLink);

        // Gets a starting space for the player
        Space playerSpace = MapController.GetSpace(1, 4);

        this.player = new Player(playerSpace);
        modelLink.CreatePlayerView(player);
    }

    public GameObject GetSpaceView()
    {
        return spaceView;
    }
    public GameObject GetPlayerView()
    {
        return playership;
    }



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
