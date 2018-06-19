using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : ScriptableObject, ISpaceCallback
{
    public int row { get; private set; }
    public int column { get; private set; }
    public GameObject spaceView;

    public static SpaceController Create(int row, int column, GameObject toSpawn)
    {
        SpaceController spaceControl = CreateInstance<SpaceController>();
        spaceControl.spaceView = Instantiate(toSpawn, new Vector2(((float)column - 1) / 2, (0 - row)), Quaternion.identity) as GameObject;
        spaceControl.spaceView.name = row.ToString() + "," + column.ToString();     // Added so as to name the SpaceHex Clones with the Space co-ordinates
        spaceControl.row = row;
        spaceControl.column = column;
        //spaceControl.spaceView.transform.GetChild(0).GetComponent<TextMesh>().text = row + "," + column;
        return spaceControl;
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
