using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DirectionSelector : MonoBehaviour
{
    [Header("Direction Settings")]
    public float minAngle = -90f;
    public float maxAngle = 90f;
    public float rotationSpeed = 2f;
    
    [Header("UI References")]
    public RectTransform arrowImage;
    
    private float currentAngle;
    private bool isActive = false;
    
    void Update()
    {
        if (!isActive) return;
        
        currentAngle = Mathf.PingPong(Time.time * rotationSpeed * 90f, maxAngle - minAngle) + minAngle;
        
        if (arrowImage != null)
        {
            arrowImage.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            SelectDirection();
        }
    }
    
    void SelectDirection()
    {
        isActive = false;
        WaveGameEvents.InvokeDirectionSelected(currentAngle);
        Deactivate();
    }
    
    public void Activate()
    {
        isActive = true;
    }
    
    public void Deactivate()
    {
        isActive = false;
    }
}
