﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    public LineRenderer lr;

    [Range(0, 1000)]
    public int segments;
    public Ellipse ellipse;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
    }

    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments) + (Vector2)gameObject.transform.position;
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }
        points[segments] = points[0];
        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (lr == null)
            lr = GetComponent<LineRenderer>();

        CalculateEllipse();
    }
}
