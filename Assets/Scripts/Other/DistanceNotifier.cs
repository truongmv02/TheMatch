using System;
using System.ComponentModel.Design;
using UnityEngine;

public class DistanceNotifier
{
    public event Action OnNotify;
    private Vector3 referencePos;
    private float sqrDistance;
    private bool enable;

    private event Action<float> notifierCondition;

    public void Init(Vector3 referencePos, float distance, bool checkInside = false, bool triggerContinuosly = false)
    {
        this.referencePos = referencePos;

        sqrDistance = distance * distance;
        if (checkInside)
        {
            notifierCondition = CheckInside;
        }
        else
        {
            notifierCondition = CheckOutside;
        }

        enable = true;

        if (!triggerContinuosly)
        {
            OnNotify += Disable;
        }
    }

    public void Disable()
    {
        enable = false;
        OnNotify -= Disable;
    }

    public void Tick(Vector3 pos)
    {
        if (!enable) return;

        var currentSqrDistance = (referencePos - pos).sqrMagnitude;
        notifierCondition.Invoke(currentSqrDistance);

    }

    private void CheckInside(float dist)
    {
        if (dist <= sqrDistance)
        {
            OnNotify?.Invoke();
        }
    }

    private void CheckOutside(float dist)
    {
        if (dist >= sqrDistance)
        {
            OnNotify?.Invoke();
        }
    }


}