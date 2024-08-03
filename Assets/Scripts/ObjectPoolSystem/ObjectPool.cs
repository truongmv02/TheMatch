using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool
{
    public abstract void ReturnObject(Component comp);
    public abstract void Release();

}

public class ObjectPool<T> : ObjectPool where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> pool = new Queue<T>();
    private readonly List<IObjectPool> allItems = new List<IObjectPool>();
    public ObjectPool(T prefab, int startCount = 0)
    {
        this.prefab = prefab;

        for (int i = 0; i < startCount; i++)
        {
            var obj = InstantiateNewObject();
            pool.Enqueue(obj);
        }
    }


    private T InstantiateNewObject()
    {
        var obj = Object.Instantiate(prefab);
        obj.name = prefab.name;

        if (!obj.TryGetComponent<IObjectPool>(out var objectPool))
        {
            Debug.LogWarning($"{obj.name} dose not have a component that implements IObjectPool");
            return obj;
        }

        objectPool.SetObjectPool(this, obj);
        allItems.Add(objectPool);

        return obj;
    }

    public T GetObject()
    {
        if (!pool.TryDequeue(out var obj))
        {
            obj = InstantiateNewObject();
            return obj;
        }

        obj.gameObject.gameObject.SetActive(true);
        return obj;
    }


    public override void Release()
    {
        foreach (var item in pool)
        {
            allItems.Remove(item as IObjectPool);
            Object.Destroy(item.gameObject);
        }

        foreach (var item in allItems)
        {
            item.Release();
        }
    }

    public override void ReturnObject(Component comp)
    {
        if (comp is not T compObj) return;

        compObj.gameObject.SetActive(false);
        pool.Enqueue(compObj);
    }
}