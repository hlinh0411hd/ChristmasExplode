using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Wall : MonoBehaviour
{
    public GameObject[] objs;

    public void Start(){

    }

    public void UpdateWallPositionToConnect(Wall wall){
        List<float> listDis = new List<float>();
        for (int i = 0; i < wall.objs.Length; i++){
            GameObject obj = wall.objs[i];
            Bounds oBound = obj.GetComponent<Renderer>().bounds;
            float dis = 1000;
            for (int j = 0; j < objs.Length; j++){
                GameObject cObj = objs[j];
                Bounds cBound = obj.GetComponent<Renderer>().bounds;
                if (Mathf.Abs(oBound.min.x - cBound.min.x) < Mathf.Abs(dis)){
                    dis = oBound.min.x - cBound.min.x;
                }
                if (Mathf.Abs(oBound.min.x - cBound.max.x) < Mathf.Abs(dis)){
                    dis = oBound.min.x - cBound.max.x;
                }
                if (Mathf.Abs(oBound.max.x - cBound.min.x) < Mathf.Abs(dis)){
                    dis = oBound.max.x - cBound.min.x;
                }
                if (Mathf.Abs(oBound.max.x - cBound.max.x) < Mathf.Abs(dis)){
                    dis = oBound.max.x - cBound.max.x;
                }
            }
            listDis.Add(dis);
        }
        float disUpdate = listDis[Random.Range(0, listDis.Count)];
        if (Mathf.Abs(disUpdate) > MapController.RANGE_WALL){
            disUpdate = (Mathf.Abs(disUpdate) - MapController.RANGE_WALL) * (disUpdate < 0? -1: 1);
        } else {
            disUpdate = 0;
        }
        wall.transform.position = new Vector2(wall.transform.position.x + disUpdate, wall.transform.position.y);
    }
}