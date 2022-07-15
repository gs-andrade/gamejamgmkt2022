using System.Collections.Generic;
using UnityEngine;


public class Pool<T> where T : Component
{
    private T prefab;
    private Queue<T> pool;
    private Transform parent;
    private int count;

    public Pool(int initialSize, T prefab, Transform parent)
    {
        this.pool = new Queue<T>();
        this.prefab = prefab;
        this.parent = parent;
        count = 0;

        for (int i = 0; i < initialSize; i++)
        {
            AddToPool(CreateInstance());
        }
    }

    public T GetInstance()
    {
        T instance = null;

        if (pool.Count > 0)
        {
            instance = pool.Dequeue();
        }
        else
        {
            instance = CreateInstance();
        }

        instance.gameObject.SetActive(true);
        return instance;
    }

    private T CreateInstance()
    {
        count++;
        var instance = GameObject.Instantiate(prefab, parent) as T;
        instance.name += count.ToString();
        return instance;
    }

    public void AddToPool(T instance, bool setActive = false)
    {
        instance.gameObject.SetActive(setActive);
        pool.Enqueue(instance);
    }
}