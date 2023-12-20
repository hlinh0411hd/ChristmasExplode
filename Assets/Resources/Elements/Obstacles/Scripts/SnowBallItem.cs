using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SnowBallItem : Item
{

    protected override void OnActiveItem(){
        PlayerController.instance.AddNumSnow(5);
    }

}