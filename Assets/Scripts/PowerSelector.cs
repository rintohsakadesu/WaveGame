using UnityEngine;
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
        
        if (powerSlider != null)
        {
            powerSlider.value = currentPowerPercent;
        }
    }
    
    public void ConfirmSelection()
    {
        if (!isActive) return;
        
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
