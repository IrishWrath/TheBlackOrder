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
    public float speed = 0.2f;
    public float length = 0.5f;

    private Vector2 startPoint, goalPoint, direction, currentStartPoint;

    private float distanceTravelled = 0f;

    private float lengthOfLine;

    public GameObject explosionPrefab;

	public void SetLine(Vector2 start, Vector2 end)
    {
        startPoint = start;
        currentStartPoint = startPoint;
        goalPoint = end;
        direction = (goalPoint - startPoint).normalized;
        lengthOfLine = (goalPoint - startPoint).magnitude;
        setLine();
    }

	private void setLine()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, currentStartPoint);
        lineRenderer.SetPosition(1, currentStartPoint + direction * length);



        if (length + (distanceTravelled * lengthOfLine) >= lengthOfLine)
        {
            lineRenderer.SetPosition(1, goalPoint);
        }
        lineRenderer.enabled = true;
    }
	
	void Update () {

        distanceTravelled += speed * Time.deltaTime;
        currentStartPoint = Vector2.Lerp(startPoint, goalPoint, distanceTravelled);

        setLine();

        if (distanceTravelled >= 1)
        {
            // TODO: create an explosion
            Instantiate(explosionPrefab, goalPoint, Quaternion.identity);

            Destroy(this.gameObject);
        }

    }
}