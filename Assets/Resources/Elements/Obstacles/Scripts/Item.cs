using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == GameConfig.TAG_PLAYER){
            OnActiveItem();
            Destroy(gameObject);
        }
    }

    protected virtual void OnActiveItem(){

    }

}