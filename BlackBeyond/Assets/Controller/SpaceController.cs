using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour, ISpaceCallback
{
    
    public GameObject spaceView;

    private Space space;

    public void SetSpace(Space space)
    {
        this.space = space;
    }

    public Space GetSpace()
    {
        return space;
    }

    public Vector2 GetPosition()
    {
        return spaceView.transform.position;
    }
}
