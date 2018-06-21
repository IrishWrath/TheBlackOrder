using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour, ISpaceCallback
{
    
    private GameObject spaceView;

    public Sprite selectedSprite;

    private Space space;

    public static void Setup(SpaceController spaceControl, int row, int column, GameObject toSpawn)
    {
        //SpaceController spaceControl = CreateInstance<SpaceController>();
        //spaceControl.spaceView = Instantiate(toSpawn, new Vector2(((float)column - 1) / 2, (0 - row)), Quaternion.identity) as GameObject;
        //spaceControl.spaceView.name = row.ToString() + "," + column.ToString();     // Added so as to name the SpaceHex Clones with the Space co-ordinates
        //spaceControl.row = row;
        //spaceControl.column = column;
        //spaceControl.spaceView.transform.GetChild(0).GetComponent<TextMesh>().text = row + "," + column;
    }

    public void SetSpace(Space space)
    {
        this.space = space;
    }

    public Space GetSpace()
    {
        return space;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpaceView(GameObject spaceView)
    {
        this.spaceView = spaceView;
    }

    public Vector2 GetPosition()
    {
        return spaceView.transform.position;
    }

    public void SetSelectable(int number)
    {
        spaceView.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }
}
