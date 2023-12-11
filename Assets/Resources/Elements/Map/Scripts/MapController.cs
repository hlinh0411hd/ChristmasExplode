using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public const float RANGE_WALL = 3;
    public static MapController instance = null;
    public GameObject ground;
    public Wall[] wallPrefabs;
    List<GameObject> crrLevelWalls;
    int numWall = 0;

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

    public void SetUp(){
        ResetNumWall();
        UpdatePositionGround();
        SetUpWalls();
    }
    
    void ResetNumWall(){
        numWall = 0;
        crrLevelWalls.Clear();
    }

    void SetUpWalls(){
        float y = ground.transform.position.y;
        Wall oldWall = null;
        while (y < CameraControl.instance.GetHeightScene() - 2){
            y += 1;
            GameObject wall = Instanciate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
            wall.transform.position = new Vector2(Random.Range(-10, 10), y);
            if (oldWall != null){
                oldWall.UpdateWallPositionToConnect(wall.GetComponent<Wall>();
            }
            numWall += 1;
            crrLevelWalls.Add(wall);
        }
    }

    void UpdatePositionGround(){
        int level = GameController.instance.level;
        ground.transform.position = new Vector2(ground.transform.position.x, level * GameConfig.SCREEN_HEIGHT);
    }
}
