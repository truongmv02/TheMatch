using System.Collections;
using UnityEngine;


public class ObjectPoolItem : MonoBehaviour, IObjectPool
{
    private ObjectPool objectPool;
    private Component component;

    public void ReturnItem(float delay = 0f)
    {
        if (delay > 0f)
        {
            StartCoroutine(ReturnItemWithDelayTime(delay));
            return;
        }

        ReturnItemToPool();
    }


    public void ReturnItemToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(component);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ReturnItemWithDelayTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        ReturnItemToPool();
    }



    public void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component
    {
        objectPool = pool;
        component = GetComponent(comp.GetType());
    }

    public void Release()
    {
        objectPool = null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}