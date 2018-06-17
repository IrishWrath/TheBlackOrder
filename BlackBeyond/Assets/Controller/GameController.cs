using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public Map Map { get; private set; }

    private IModelLink modelLink;
    public GameObject toSpawn;
    public GameObject ship;
    public Material selectColour;

    // Use this for initialization
    void Start () 
    {
        this.modelLink = new ModelLink(toSpawn);
        this.Map = new Map(5, 5, modelLink);

        ShipController.Create(Map.GetSpace(1,4), ship);
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
