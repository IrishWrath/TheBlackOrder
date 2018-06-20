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
		
	}
}
