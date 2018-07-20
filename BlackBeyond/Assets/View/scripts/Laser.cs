using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// To create a laser, you instantiate the laser prefab, then use the SetLine
/// function to choose the start and end points.
/// </summary>
public class Laser : MonoBehaviour {

    public LineRenderer lineRenderer;

    //some values
    public float speed = 2f;
    public float length = 0.5f;

    //some position variables
    private Vector2 lineStart, lineEnd, direction, goalPoint;

	public void SetLine(Vector2 start, Vector2 end)
    {
        SetStartPoint(start);

        goalPoint = end;
        direction = (goalPoint - lineStart).normalized;
    }

	private void SetStartPoint(Vector2 start)
    {
        lineStart = start;
        lineRenderer.SetPosition(0, lineStart);
    }

    // this is used to set the end point of the line.
    private void SetEndPoint(Vector2 end)
    {
        lineEnd = end;
        //TODO: Need to clamp to goal point.
        lineRenderer.SetPosition(1, lineEnd);
    }
	
	void Update () {
        // move the points along the line (direction)
        SetStartPoint(lineStart + (direction * speed * Time.deltaTime));
        SetEndPoint(lineStart + direction * length);

        //TODO: Create an explosion when reached goal point
	}
}
