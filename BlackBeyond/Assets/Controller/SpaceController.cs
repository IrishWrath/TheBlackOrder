using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour, ISpaceCallback
{
    public int row { get; private set; }
    public int column { get; private set; }
    public GameObject spaceView;

    public static void Setup(SpaceController spaceControl, int row, int column, GameObject toSpawn)
    {
        //SpaceController spaceControl = CreateInstance<SpaceController>();
        //spaceControl.spaceView = Instantiate(toSpawn, new Vector2(((float)column - 1) / 2, (0 - row)), Quaternion.identity) as GameObject;
        spaceControl.row = row;
        spaceControl.column = column;
        //spaceControl.spaceView.transform.GetChild(0).GetComponent<TextMesh>().text = row + "," + column;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void turnColour(Material selectColour)
    {
        spaceView.GetComponent<MeshRenderer>().material = selectColour;
    }

    public Vector2 GetPosition()
    {
        return spaceView.transform.position;
    }
}
