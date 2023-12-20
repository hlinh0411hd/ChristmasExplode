using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActivatorItem : Item
{

    protected override void OnActiveItem(){
        PlayerController.instance.IncreaseActivator();
        FreezeEnemy();
    }

    void FreezeEnemy(){
        EnemyController.instance.FreezeAll();
    }

}