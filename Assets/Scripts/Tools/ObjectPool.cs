using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : Component
{
    private List<T> pool = new();

    public T PopOrCreate(T prefab, Transform parent = null, Predicate<T> predicate = null)
    {
        var instance = pool.FirstOrDefault(x => !x.gameObject.activeSelf && (predicate == null || predicate(x)));
        instance ??= Object.Instantiate(prefab, parent);
        pool.Remove(instance);
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Add(T instance)
    {
        instance.gameObject.SetActive(false);
        pool.Add(instance);
    }
}
