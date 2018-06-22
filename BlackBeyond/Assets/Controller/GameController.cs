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
    public void Start()
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
                    ShipController ShC = new ShipController();       // Create required instance of ShipController class
                    
                    Space destination = hit.transform.gameObject.GetComponent<SpaceController>().GetSpace();        // Convert vector2 location of spacehex to a space and then assign to destination
                    
                    //Debug.Log("Destination Co-ords: " + destination.Row + ":" + destination.Column);
                    //Debug.Log("Destination Vector2: " + destination.GetCallback().GetPosition().ToString());

                    ShC.moveShip(destination);       // call moveShip function in shipcontroller and pass destination space
                }
            }
        }
    }
}
