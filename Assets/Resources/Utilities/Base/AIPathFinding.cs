using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AIPathFinding : MonoBehaviour
{


    public List<List<int>> mapPath;
    public List<List<int>> mapPathLength;

    private void Awake()
    {
        mapPath = new List<List<int>>();
        mapPathLength = new List<List<int>>();
    }

    private void Start()
    {
    }

    public void FindPathToTile(Vector2Int tile, Vector2Int size)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        mapPath.Clear();
        mapPathLength.Clear();
        for (int i = 0; i < MapController.instance.currentMap.width; i++)
        {
           List<int> list = new List<int>();
           List<int> listLength = new List<int>();
           for (int j = 0; j < MapController.instance.currentMap.height; j++)
           {
               list.Add((int)DIRECTION.NULL);
               listLength.Add(100000);
           }
           mapPath.Add(list);
           mapPathLength.Add(listLength);
        }
        for (int i = 0; i < size.x; i++){
            for (int j = 0; j < size.y; j++){
                queue.Enqueue(new Vector2Int(tile.x + i, tile.y + j));
                mapPathLength[tile.x + i][tile.y + j] = 0;
                mapPath[tile.x + i][tile.y + j] = (int)DIRECTION.CENTER;
            }
        }
        while (queue.Count > 0)
        {
           Vector2Int pos = queue.Dequeue();
           for (int i = 0; i < GameConfig.DIR_X.Length; i += 1)
           {
               Vector2Int newPos = pos + new Vector2Int(GameConfig.DIR_X[i], GameConfig.DIR_Y[i]);
               if (mapPath[newPos.x][newPos.y] != (int)DIRECTION.NULL)
               {
                   if (mapPathLength[newPos.x][newPos.y] != 100000 && mapPathLength[newPos.x][newPos.y] <= mapPathLength[pos.x][pos.y] + MapController.instance.currentMap.GetPointTileForPath(newPos))
                   {
                       continue;
                   }
               }
               if (MapController.instance.currentMap.CheckEmpty(newPos, new Vector2Int(1, 1)))
               {
                   mapPath[newPos.x][newPos.y] = (i + 4) % 8;
                   mapPathLength[newPos.x][newPos.y] = mapPathLength[pos.x][pos.y] + MapController.instance.currentMap.GetPointTileForPath(newPos);
                   queue.Enqueue(newPos);
               }
           }
        }

        //if (gameObject.tag != GameConfig.TAG_HERO && GetComponent<AIAction>().GetTarget().tag != GameConfig.TAG_STAIR)
        //{
        //    return;
        //}
        //Debug.Log("Hero Map");
        //for (int i = 0; i < mapPath.Count; i++)
        //{
        //    string s = "";
        //    for (int j = 0; j < mapPath[i].Count; j++)
        //    {
        //        for (int k = 0; k < 3 - mapPath[i][j].ToString().Length; k++)
        //        {
        //            s += "_";
        //        }
        //        s += mapPath[i][j].ToString();
        //    }
        //    Debug.Log(s);
        //}
        // Debug.Log("Hero Map LENGTH");
        // for (int i = 0; i < mapPathLength.Count; i++)
        // {
        //     string s = "";
        //     for (int j = 0; j < mapPathLength[i].Count; j++)
        //     {
        //         string c = mapPathLength[i][j] < 100000 ? mapPathLength[i][j].ToString() : "-1";
        //         for (int k = 0; k < 7 - c.Length; k++)
        //         {
        //             s += "_";
        //         }
        //         s += c.ToString();
        //     }
        //     Debug.Log(s);
        // }
    }
}
