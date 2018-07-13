using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }

    // A model link.
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

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
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

    public void PlayerMoveButton()
    {
        //TODO Call playership.startmove() or similar
    }

    // This function is called whe the player presses "end turn"
    public void EndTurn()
    {
        playerModel.EndTurn();

        // End of turn Housekeeping

        // Pirates move
        // Foreach pirate in map.GetPirates()
        //      pirate.DoTurn();
        // Or something...

        playerModel.StartTurn();
    }

    // This Update should be avoided. Only place testing code here.
    // Update is called once per frame
    void Update()
    {
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

	private void OnGUI()
	{
        Event newEvent = Event.current;
        if(newEvent.keyCode == KeyCode.Return)
        {
            EndTurn();
        }
	}
}
