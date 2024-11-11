//using Oculus.Interaction.HandGrabAPI;
using UnityEngine;

public class HandDrawing : MonoBehaviour
{/*
    public HandGrabAPI handGrabAPI;
    public Transform fingertip;
    public LineRenderer lineRenderer;
    private bool isDrawing = false;

    void Update()
    {
        if (handGrabAPI != null && handGrabAPI.IsPinching())
        {
            if (!isDrawing)
            {
                StartDrawing();
            }
            AddPointToLine(fingertip.position);
        }
        else
        {
            isDrawing = false;
        }
    }

    void StartDrawing()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 0;
        isDrawing = true;
    }

    void AddPointToLine(Vector3 position)
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
        }
    }*/
}
