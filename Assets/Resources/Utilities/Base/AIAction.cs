using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAction : MonoBehaviour
{
    public class TypeFindTarget
    {
        public const int NEAREST = 0;
        public const int LOWEST_HEALTH = 1;
        public const int HIGHEST_HEALTH = 2;
        public const int ITEM = 3;
        public const int LOWEST_DAMAGE = 4;
        public const int LESS_GROUP = 5;
        public const int BIGGEST_GROUP = 6;
		public const int PRIORITY = 7;
    }


    protected GameObject target;
    protected Vector2Int targetPos;
    protected Vector2Int crrTargetPos;
    protected int typeFindTarget;

    protected AIPathFinding aIPathFinding;

    protected void Awake()
    {
        aIPathFinding = GetComponent<AIPathFinding>();
    }


    protected virtual List<string> GetTargetsByTag()
    {
        List<string> list = new List<string>();
        return list;
    }
    protected virtual List<string> GetEnemiesByTag()
    {
        List<string> list = new List<string>();
        return list;
    }

    protected void ChooseTypeTarget(int type){
        typeFindTarget = type;
    }

    public void FindTarget()
    {
        target = null;
        List<string> targetTags = GetTargetsByTag();
        switch (typeFindTarget)
        {
            case TypeFindTarget.NEAREST:
                {
                    float dis = 30000;
                    for (int i = 0; i < targetTags.Count; i++)
                    {
                        string tag = targetTags[i];
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                        foreach (GameObject go in gameObjects)
                        {
                            if (go.GetComponent<SpriteRenderer>().enabled == false) continue;
                            float difDis = Vector2.Distance(transform.position, go.transform.position);
                            if (difDis < dis)
                            {
                                dis = difDis;
                                target = go;
                            }
                        }
                    }
                    break;
                }
            case TypeFindTarget.LOWEST_HEALTH:
                {
                    float health = 1000;
                    float dis = 30000;
                    for (int i = 0; i < targetTags.Count; i++)
                    {
                        string tag = targetTags[i];
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                        foreach (GameObject go in gameObjects)
                        {
                            if (go.GetComponent<Health>() == null) break;
                            if (go.GetComponent<SpriteRenderer>().enabled == false) continue;
                            float goHealth = go.GetComponent<Health>().health;
                            float difDis = Vector2.Distance(transform.position, go.transform.position);
                            if (goHealth < health)
                            {
                                health = goHealth;
                                target = go;
                                dis = difDis;
                            }
                            else if (goHealth == health)
                            {
                                if (difDis < dis)
                                {
                                    dis = difDis;
                                    health = goHealth;
                                    target = go;
                                }
                            }
                        }
                    }
                    break;
                }
            case TypeFindTarget.HIGHEST_HEALTH:
                {
                    float health = 0;
                    float dis = 30000;
                    for (int i = 0; i < targetTags.Count; i++)
                    {
                        string tag = targetTags[i];
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                        foreach (GameObject go in gameObjects)
                        {
                            if (go.GetComponent<Health>() == null) break;
                            if (go.GetComponent<SpriteRenderer>().enabled == false) continue;
                            float goHealth = go.GetComponent<Health>().health;
                            float difDis = Vector2.Distance(transform.position, go.transform.position);
                            if (goHealth > health)
                            {
                                health = goHealth;
                                target = go;
                            }
                            else if (goHealth == health)
                            {
                                if (difDis < dis)
                                {
                                    dis = difDis;
                                    health = goHealth;
                                    target = go;
                                }
                            }
                        }
                    }
                    break;
                }
            case TypeFindTarget.ITEM:
                {
                    float dis = 30000;
                    for (int i = 0; i < targetTags.Count; i++)
                    {
                        string tag = targetTags[i];
                        if (tag != GameConfig.TAG_ITEM) continue;
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                        foreach (GameObject go in gameObjects)
                        {
                            if (go.GetComponent<SpriteRenderer>().enabled == false) continue;
                            float difDis = Vector2.Distance(transform.position, go.transform.position);
                            if (difDis < dis)
                            {
                                dis = difDis;
                                target = go;
                            }
                        }
                    }
                    break;
                }
            case TypeFindTarget.PRIORITY:
                {
                    float dis = 30000;
                    for (int i = 0; i < targetTags.Count; i++)
                    {
                        string tag = targetTags[i];
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                        foreach (GameObject go in gameObjects)
                        {
                            if (go.GetComponent<SpriteRenderer>().enabled == false) continue;
                            float difDis = Vector2.Distance(transform.position, go.transform.position);
                            if (difDis < dis)
                            {
                                dis = difDis;
                                target = go;
                            }
                        }
						if (target != null){
							return;
						}
                    }
                    break;
                }
        }
        return;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public Vector2Int GetTargetPos()
    {
        return crrTargetPos;
    }

    public virtual float GetRangeAction(string tag)
    {
        return 0;
    }
}
