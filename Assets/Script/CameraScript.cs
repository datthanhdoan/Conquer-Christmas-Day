using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] Tilemap tileMap;
    [SerializeField] private float _buffer = 1.5f;
    private void Start()
    {
        var (center, size) = CalculateOrthoSize();
        _cam.transform.position = center;
        _cam.orthographicSize = size;
    }
    private (Vector3 center, float size) CalculateOrthoSize()
    {
        tileMap.CompressBounds();
        var bounds = tileMap.localBounds;
        bounds.Expand(_buffer);
        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * (_cam.pixelHeight / _cam.pixelWidth);

        var size = Mathf.Max(vertical, horizontal) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);
        return (center, size);
    }
}