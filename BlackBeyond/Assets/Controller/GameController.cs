using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public MapController MapController { get; private set; }

    private IModelLink modelLink;
    public GameObject spaceView;
    public GameObject ship;
    public Material selectColour;

    // Use this for initialization
    public void Start () 
    {
        this.modelLink = new ModelLink(this);
        this.MapController = new MapController(5, 5, modelLink);

        //ShipController.Create(MapController.GetSpace(1,4), ship);
        Space playerSpace = MapController.GetSpace(1, 4);
        GameObject playerView = Instantiate(ship, playerSpace.GetCallback().GetPosition(), Quaternion.identity);
    }

    public GameObject GetSpaceView()
    {
        return spaceView;
    }

    // Update is called once per frame
	void Update () 
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
                    // Oisín: Not getting by name, below gets the space.
                    //string spacehexName = hit.transform.name;
                    //int row = int.Parse(spacehexName[0].ToString());
                    //int column = int.Parse(spacehexName[2].ToString());
                    //Space newSpace = MapController.GetSpace(row, column);

                    // Oisín: Gets the space that the Ray hit, could get the controller instead.
                    Space newSpace = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();

                    Debug.Log("Space Co-ords: " + newSpace.Row + ":" + newSpace.Column);
                    Debug.Log("Space Vector2: " + newSpace.GetCallback().GetPosition().ToString());

                    // Oisín: I think it would be best to call a move method in ShipController (Not my one though. That doesn't work yet.)
                    //ship.gameObject.transform.position = newSpace.GetCallback().GetPosition();


                    List<PathfindingNode> nodes =  new DijkstrasPathfinding(newSpace, 1).GetNodes();
                    foreach(PathfindingNode node in nodes)
                    {
                        node.GetSpace().GetCallback().SetSelectable(node.GetCost());
                    }

                }
            }
        }
	}
}
