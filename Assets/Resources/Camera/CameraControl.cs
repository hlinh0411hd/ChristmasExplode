using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;

public class CameraControl : MonoBehaviour
{

    public static CameraControl instance;

    public GameObject player;

    public bool isEnd = false;

    public float heightView = 0;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
        Camera camera = Camera.main;
        Vector3 p0 = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        Vector3 p1 = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        heightView = p0.y - p1.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
    }

    public void SetUp()
    {
        player = PlayerController.instance.GetPlayer();
        UpdateCameraPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void UpdateCameraPosition()
    {
        if (!player)
        {
            return;
        }
        Camera camera = Camera.main;
        float pY = player.transform.position.y;
        float cY = camera.transform.position.y;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(camera.transform.DOMoveY(camera.transform.position.y + (pY - cY) + heightView / 2 - 1, 1f));
    }

    public float GetHeightScene(){
        Camera camera = Camera.main;
        Vector3 p = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        return p.y;
    }
}
