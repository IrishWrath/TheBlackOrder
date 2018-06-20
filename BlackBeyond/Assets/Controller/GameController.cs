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
                    string spacehexName = hit.transform.name;
                    int row = int.Parse(spacehexName[0].ToString());
                    int column = int.Parse(spacehexName[2].ToString());

                    Space newSpace = Map.GetSpace(row, column);

                    Debug.Log("Space Co-ords: " + spacehexName);
                    Debug.Log("Space Vector2: " + newSpace.GetPosition().ToString());

                    ship.gameObject.transform.position = newSpace.GetPosition();
                }
            }
        }
	}
}
