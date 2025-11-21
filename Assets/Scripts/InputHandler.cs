using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private WaveGameManager gameManager;
    private DirectionSelector directionSelector;
    private PowerSelector powerSelector;
    
    void Start()
    {
        gameManager = FindFirstObjectByType<WaveGameManager>();
        directionSelector = FindFirstObjectByType<DirectionSelector>();
        powerSelector = FindFirstObjectByType<PowerSelector>();
    }
    
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame || 
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame))
        {
            HandleClick();
        }
    }
    
    void HandleClick()
    {
        switch (gameManager.currentState)
        {
            case WaveGameManager.GameState.SelectingDirection:
                directionSelector?.ConfirmSelection();
                break;
                
            case WaveGameManager.GameState.SelectingPower:
                powerSelector?.ConfirmSelection();
                break;
                
            case WaveGameManager.GameState.WaveActive:
                break;
        }
    }
}
