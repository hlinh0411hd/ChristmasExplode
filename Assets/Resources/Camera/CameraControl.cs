using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{

    public static CameraControl instance;

    public GameObject player;

    public bool isEnd = false;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
    }

    public void SetUp()
    {
        player = PlayerController.instance.GetPlayer();
        //baseCrystal = CrystalController.instance.GetBase();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player)
        {
            return;
        }
        Camera camera = Camera.main;
        float height = camera.orthographicSize;
        int level = GameController.instance.level;
        Vector3 pos = new Vector3(transform.position.x, ((float) level + 0.5f) * height, 0);
        transform.position = pos;
    }
}
