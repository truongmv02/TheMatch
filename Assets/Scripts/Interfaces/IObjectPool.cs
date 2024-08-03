using UnityEngine;

public interface IObjectPool
{
    void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;
    void Release();
}