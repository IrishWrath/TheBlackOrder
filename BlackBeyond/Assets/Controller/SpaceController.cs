using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour, ISpaceCallback
{
    
    private GameObject spaceView;

    public Sprite selectedSprite;

    private Space space;

    public void SetSpace(Space space)
    {
        this.space = space;
    }

    public Space GetSpace()
    {
        return space;
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
