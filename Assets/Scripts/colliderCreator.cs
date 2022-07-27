using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ColliderCreator : MonoBehaviour
{
    public PolygonCollider2D poly;

    // Start is called before the first frame update
    void Awake()
    {
        Vector2[] points = poly.points;
        Vector2 firstPoint = points[0];
        Array.Resize(ref points, points.Length + 1);
        points[points.Length - 1] = firstPoint;
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(poly);
    }
}
