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

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
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
        UpdatePositionGround();
        SetUpWalls();
    }

    void SetUpWalls(){
        
    }

    void UpdatePositionGround(){
        int level = GameController.instance.level;
        ground.transform.position = new Vector2(ground.transform.position.x, level * GameConfig.SCREEN_HEIGHT);
    }
}
