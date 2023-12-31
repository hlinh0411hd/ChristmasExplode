using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public const float RANGE_WALL = 1;
    public const float RANGE_HEIGHT_WALL = 2.5f;
    public static MapController instance = null;
    public GameObject grid;
    public GameObject ground;
    public GameObject[] wallPrefabs;
    List<GameObject> crrLevelWalls;
    int numWallMap;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
        crrLevelWalls = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        numWallMap = Mathf.FloorToInt((CameraControl.instance.heightView - RANGE_HEIGHT_WALL) / RANGE_HEIGHT_WALL);
        SetUpWalls();
        UpdatePositionGround();
    }

    void SetUpWalls(){
        float y = ground.transform.position.y;
        crrLevelWalls.Clear();
        Wall oldWall = null;
        for (int i = 0; i < numWallMap; i++){
            y += RANGE_HEIGHT_WALL;
            GameObject wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
            wall.transform.position = new Vector2(Random.Range(-10, 10), y);
            if (oldWall != null){
                oldWall.UpdateWallPositionToConnect(wall.GetComponent<Wall>());
            }
            oldWall = wall.GetComponent<Wall>();
            wall.transform.parent = grid.transform;
            crrLevelWalls.Add(wall);
        }
    }

    public GameObject GetWallDemon(){
        for (int i = crrLevelWalls.Count - 1; i >= 0; i--)
        {
            GameObject wOb = crrLevelWalls[i];
            if (wOb.transform.position.y <= Camera.main.transform.position.y + CameraControl.instance.heightView / 2)
            {
                Wall wall = wOb.GetComponent<Wall>();
                return wall.GetRandomWall();
            }
        }
        return null;
    }

    void UpdatePositionGround(){
        int level = GameController.instance.level;
        ground.transform.position = new Vector2(ground.transform.position.x, level * CameraControl.instance.heightView);
    }
}
