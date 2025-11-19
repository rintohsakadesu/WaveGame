using UnityEngine;

public class WaveGameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject wavePrefab;
    public DirectionSelector directionSelector;
    public PowerSelector powerSelector;
    public CameraFollow cameraFollow;
    public Transform spawnPoint;
    
    [Header("Spawn Settings")]
    public Vector3 spawnOffset = new Vector3(0f, 0f, 0f);
    
    private float selectedAngle;
    private GameObject currentWave;
    
    void OnEnable()
    {
        WaveGameEvents.OnDirectionSelected += HandleDirectionSelected;
        WaveGameEvents.OnPowerSelected += HandlePowerSelected;
    }
    
    void OnDisable()
    {
        WaveGameEvents.OnDirectionSelected -= HandleDirectionSelected;
        WaveGameEvents.OnPowerSelected -= HandlePowerSelected;
    }
    
    void Start()
    {
        StartNewCycle();
    }
    
    void HandleDirectionSelected(float angle)
    {
        selectedAngle = angle;
        
        if (directionSelector != null)
        {
            directionSelector.Deactivate();
        }
        
        if (powerSelector != null)
        {
            powerSelector.Activate();
        }
    }
    
    void HandlePowerSelected(float power)
    {
        if (powerSelector != null)
        {
            powerSelector.Deactivate();
        }
        
        SpawnWave(selectedAngle, power);
    }
    
    void SpawnWave(float angle, float power)
    {
        if (wavePrefab == null) return;
        
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : spawnOffset;
        currentWave = Instantiate(wavePrefab, spawnPosition, Quaternion.identity);
        
        WaveController waveController = currentWave.GetComponent<WaveController>();
        if (waveController == null)
        {
            waveController = currentWave.AddComponent<WaveController>();
        }
        
        Vector3 direction = GetDirectionFromAngle(angle);
        waveController.Initialize(direction, power);
        
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(currentWave.transform);
        }
        
        WaveGameEvents.InvokeWaveSpawned(direction, power);
        
        Invoke(nameof(OnWaveLifetimeEnded), waveController.lifetime + 1f);
    }
    
    void OnWaveLifetimeEnded()
    {
        WaveGameEvents.InvokeWaveDestroyed();
        StartNewCycle();
    }
    
    void StartNewCycle()
    {
        if (directionSelector != null)
        {
            directionSelector.Activate();
        }
    }
    
    Vector3 GetDirectionFromAngle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians)).normalized;
    }
}
