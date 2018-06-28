using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls a single space
public class SpaceController : MonoBehaviour
{
    // The view of this controller
    private GameObject spaceView;

    // TODO put a selected sprite in here.
    public Sprite selectedSprite;

    // The model of this controller
    private Space space;

    // Sets the model
    public void SetSpace(Space space)
    {
        this.space = space;
    }

    // Returns the model
    public Space GetSpace()
    {
        return space;
    }

    // Sets the view
    public void SetSpaceView(GameObject spaceView)
    {
        this.spaceView = spaceView;
    }

    // Returns the view's position
    public Vector2 GetPosition()
    {
        return spaceView.transform.position;
    }

    // Changes the sprite to match some event. TODO method
    public void SetSelectable(int number)
    {
        spaceView.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }
}
