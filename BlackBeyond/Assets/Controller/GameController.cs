using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Map map { get; private set; }

    private ModelLink modelLink;
    public GameObject toSpawn;
    public GameObject ship;
    public Material selectColour;

    // Use this for initialization
    void Start () {
        this.modelLink = new ModelLink(toSpawn);
        this.map = new Map(5, 5, modelLink);
       

        //Space[][] newMap = map.getMap();
        //foreach(Space[] row in newMap)
        //{
        //    foreach(Space column in row)
        //    {
        //        if (column != null)
        //        {
        //            SpaceController.Create(column.Row, column.Column, modelLink);
        //        }
        //    }
        //}

        ShipController.Create(map.GetSpace(1,4), ship);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
