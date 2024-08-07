using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    CannonController cannonController;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 100;

    // Distance between those points on the line
    public float timeBetweenPoints = 0.05f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask collidableLayers;

    void Start()
    {
        cannonController = GetComponent<CannonController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;  // Line is off by default
    }

    void Update()
    {
        if (!lineRenderer.enabled) return;  // Don't calculate if line is off

        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = cannonController.ShotPoint.position;
        Vector3 startingVelocity = cannonController.ShotPoint.forward * cannonController.BlastPower;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }

    public void ShowLine()
    {
        lineRenderer.enabled = true;  // Show the line
    }

    public void HideLine()
    {
        lineRenderer.enabled = false;  // Hide the line
    }
}
