using UnityEngine;
using System.Collections.Generic;

public static class GenericNotImplementedError<T>
{
    public static T TryGet(T value, string name)
    {
        if (value != null) return value;
        Debug.LogError(typeof(T) + " is not implemented on " + name);
        return default;
    }
}