using System;
using UnityEngine;
using UnityEngine.Events;

public static class WaveGameEvents
{
    public static event Action<float> OnDirectionSelected;

    public static void InvokeDirectionSelected(float angle)
    {
        OnDirectionSelected?.Invoke(angle);
    }

    public static event Action<float> OnPowerSelected;

    public static void InvokePowerSelected(float power)
    {
        OnPowerSelected?.Invoke(power);
    }

    public static event Action<Vector3, float> OnWaveSpawned;

    public static void InvokeWaveSpawned(Vector3 direction, float power)
    {
        OnWaveSpawned?.Invoke(direction, power);
    }

    public static event Action OnWaveDestroyed;
    public static void InvokeWaveDestroyed()
    {
        OnWaveDestroyed?.Invoke();
    }
}
