using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoundingBox
{
    public Vector2Int position;
    public Vector2Int size;
    public Vector2 anchor;
    public BoundingBox()
    {
        this.position = new(0, 0);
        this.size = new(0, 0);
        this.anchor = new(0, 0);
    }

    public BoundingBox(Vector2Int position, Vector2Int size, Vector2 anchor)
    {
        this.position = position;
        this.size = size;
        this.anchor = anchor;
    }
    public BoundingBox(Vector2Int position, Vector2Int size)
    {
        this.position = position;
        this.size = size;
        this.anchor = new Vector2(0, 0);
    }

    public static BoundingBox ConvertAnchorToZero(BoundingBox box)
    {
        BoundingBox newBox = new BoundingBox(box.position, box.size, new Vector2(0, 0));
        int difX = Mathf.FloorToInt((0 - box.anchor.x) * box.size.x);
        int difY = Mathf.FloorToInt((0 - box.anchor.y) * box.size.y);
        newBox.position.x += difX;
        newBox.position.y += difY;
        return newBox;
    }
    public static BoundingBox ConvertAnchorToMid(BoundingBox box)
    {
        BoundingBox newBox = new BoundingBox(box.position, box.size, new Vector2(0.5f, 0.5f));
        int difX = Mathf.FloorToInt((0.5f - box.anchor.x) * box.size.x);
        int difY = Mathf.FloorToInt((0.5f - box.anchor.y) * box.size.y);
        newBox.position.x += difX;
        newBox.position.y += difY;
        return newBox;
    }
}
