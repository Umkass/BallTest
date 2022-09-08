using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDrawer : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Draw(Vector3 forceDiretion, float forceMagnitude)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0,Vector3.zero);
        lineRenderer.SetPosition(1, forceDiretion *forceMagnitude * 20);
    }
    public void Hide()
    {
        lineRenderer.positionCount = 0;
    }
}
