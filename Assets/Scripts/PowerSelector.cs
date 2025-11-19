using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerSelector : MonoBehaviour
{
    [Header("Power Settings")]
    public float minPower = 5f;
    public float maxPower = 20f;
    public float fillSpeed = 2f;
    
    [Header("UI References")]
    public Slider powerSlider;
    
    private float currentPowerPercent;
    private bool isActive = false;
    
    void Update()
    {
        if (!isActive) return;
        
        currentPowerPercent = Mathf.PingPong(Time.time * fillSpeed, 1f);
        Debug.Log("Power Percentage: " + currentPowerPercent);
        
        if (powerSlider != null)
        {
            powerSlider.value = currentPowerPercent;
        }
        
         if (Mouse.current.leftButton.wasPressedThisFrame || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            SelectPower();
        }
    }
    
    void SelectPower()
    {
        isActive = false;
        float selectedPower = Mathf.Lerp(minPower, maxPower, currentPowerPercent);
        WaveGameEvents.InvokePowerSelected(selectedPower);
        Deactivate();
    }
    
    public void Activate()
    {
        isActive = true;
        currentPowerPercent = 0f;
    }
    
    public void Deactivate()
    {
        isActive = false;
    }
}
