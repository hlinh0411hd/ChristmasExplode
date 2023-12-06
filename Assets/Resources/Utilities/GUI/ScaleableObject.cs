using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleableObject : MonoBehaviour
{

    public float rate;
    public float rateX;
    public float rateY;

    private RectTransform rect;

    private float timeMinToMax = 0.1f;
    private float timeMaxToNormal = 0.05f;
    private float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("SIZE " + Screen.width + " " + Screen.height);
        currentTime = timeMaxToNormal + timeMinToMax;
        rateX = 1f * Screen.width / GameConfig.SCREEN_WIDTH;
        rateY = 1f * Screen.height / GameConfig.SCREEN_HEIGHT;
        rate = Mathf.Min(rateX, rateY);
        rect = gameObject.GetComponent<RectTransform>();
        if (rect != null)
        {
            /*Vector2 size = new Vector2(rect.rect.width * rate, rect.rect.height * rate);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);*/
            rect.localScale = new Vector3(rate, rate, rect.localScale.z);
            Debug.Log("" + rect.position.x + " " + rect.position.y + " " + rateX + " " + rateY);
            Vector3 pos = rect.position;
            pos.x = Screen.width * rect.anchorMax.x - (Screen.width * rect.anchorMax.x - pos.x) * rate;
            pos.y = Screen.height * rect.anchorMax.y - (Screen.height * rect.anchorMax.y - pos.y) * rate;
            rect.position = new Vector3(pos.x, pos.y, pos.z);
        }

    }

}
