using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOrderUpdate : MonoBehaviour
{
    private float zOrderMin = -5;
    private float zOrderMax = 0;
    private float bias = 100;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 pos = transform.position;
        float sumAxes = -pos.y + bias;
        if (sumAxes < 0) sumAxes = 0;
        float zOrder = -sumAxes / 200 * (zOrderMax - zOrderMin);
        transform.position = new Vector3(pos.x, pos.y, zOrder);
    }
}
