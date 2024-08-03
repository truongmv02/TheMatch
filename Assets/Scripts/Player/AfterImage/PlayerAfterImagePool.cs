using UnityEngine;
using System.Collections.Generic;
public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private PlayerAfterImage afterImagePrefab;
    private Queue<PlayerAfterImage> availableObjects = new Queue<PlayerAfterImage>();

    public static PlayerAfterImagePool Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (var i = 0; i < 10; i++)
        {
            PlayerAfterImage instance = Instantiate(afterImagePrefab);
            instance.transform.SetParent(transform);
            AddToPool(instance);
        }
    }

    public void AddToPool(PlayerAfterImage instanceToAdd)
    {
        instanceToAdd.gameObject.SetActive(false);
        availableObjects.Enqueue(instanceToAdd);
    }
    public PlayerAfterImage GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }
}