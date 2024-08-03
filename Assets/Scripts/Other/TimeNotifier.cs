using System;
using UnityEngine;

public class TimeNotifier
{

    public event Action OnNotify;
    private float duration;
    private float targetTime;

    private bool enable;


    public void Init(float duration, bool reset = false)
    {
        enable = true;
        this.duration = duration;
        SetTargetTime();

        if (reset)
        {
            OnNotify += SetTargetTime;
        }
        else
        {
            OnNotify += Disable;
        }
    }

    private void SetTargetTime()
    {
        targetTime = Time.time + duration;
    }

    public void Disable()
    {
        enable = false;
        OnNotify -= Disable;
    }


    public void Tick()
    {
        if (!enable) return;
        if (Time.time >= targetTime)
        {
            OnNotify?.Invoke();
        }
    }
}