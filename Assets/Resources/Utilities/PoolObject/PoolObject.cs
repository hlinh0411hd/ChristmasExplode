using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> where T : Behaviour, IPool
{
    public T baseObject;
    public Queue<T> poolInactive;
    public List<T> poolActive;

    public PoolObject(T obj, int num = 10)
    {
        baseObject = obj;
        poolInactive = new Queue<T>();
        poolActive = new List<T>();
        for (int i = 0; i < num; i++)
        {
            T gO = CreateObject();
            poolInactive.Enqueue(gO);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < poolActive.Count; i++)
        {
            DestroyObject(poolActive[i]);
        }
    }

    public T CreateObject()
    {
        T clone = GameObject.Instantiate(baseObject, Vector3.zero, Quaternion.identity);
        clone.gameObject.SetActive(false);
        return clone;
    }

    public T GetObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        T obj = null;
        if (poolInactive.Count > 0)
        {
            obj = poolInactive.Dequeue();
        }
        else
        {
            obj = CreateObject();
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.parent = parent;
        obj.GetComponent<IPool>().OnSpawn();
        poolActive.Add(obj);
        return obj;

    }

    public List<T> GetObjects(int num, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < num; i++)
        {
            list.Add(GetObject(position, rotation, parent));
        }
        return list;
    }

    public void DestroyObject(T obj)
    {
        obj.gameObject.SetActive(false);
        poolInactive.Enqueue(obj);
        poolActive.Remove(obj);
        Debug.Log("NUM " + poolActive.Count + "  " + poolInactive.Count);
    }

    public void DestroyAllObjects()
    {
        while (poolActive.Count > 0)
        {
            DestroyObject(poolActive[0]);
        }
    }

    public int GetNumActive()
    {
        return poolActive.Count;
    }

}
