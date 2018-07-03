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
    public Sprite defaultSprite;
    public Sprite selectedSprite;
    public Sprite nebulaSprite;
    public Sprite mouseOverSprite;

    // The model of this controller
    private SpaceModel space;

    // Is this space selectable
    private bool selectable;

    // Sets the model
    public void SetSpace(SpaceModel space)
    {
        this.space = space;
    }

    // Returns the model
    public SpaceModel GetSpace()
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
    public void SetSelectable(int cost)
    {
        selectable = true;
        spaceView.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        spaceView.transform.GetChild(0).GetComponent<TextMesh>().text = cost.ToString();
    }

    public void Deselect()
    {
        selectable = false;
        spaceView.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        spaceView.transform.GetChild(0).GetComponent<TextMesh>().text = "";
    }

	private void OnMouseOver()
	{
        if(selectable)
        {
            spaceView.GetComponent<SpriteRenderer>().sprite = mouseOverSprite;
        }
	}

    private void OnMouseExit()
    {
        if (selectable)
        {
            spaceView.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }
    }

	// Sets this space to a nebula
	public void SetNebula()
    {
        defaultSprite = nebulaSprite;
        spaceView.GetComponent<SpriteRenderer>().sprite = nebulaSprite;
    }
}
