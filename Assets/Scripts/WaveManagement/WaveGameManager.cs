using UnityEngine;

public class WaveGameManager : MonoBehaviour
{
    [Header("References")]
    public WaveSpawner waveSpawner;
    public DirectionSelector directionSelector;
    public PowerSelector powerSelector;

    private float selectedAngle;
    private GameObject currentWave;

    public AudioClip bgpmToPlay;

    public enum GameState
    {
        SelectingDirection,
        SelectingPower,
        WaveActive
    }

    public GameState currentState;

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
        AudioManager.Instance.PlayBgm(bgpmToPlay);
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
        currentState = GameState.SelectingPower;
    }

    void HandlePowerSelected(float power)
    {
        if (powerSelector != null)
        {
            powerSelector.Deactivate();
        }

        SpawnWave(selectedAngle, power);
        currentState = GameState.WaveActive;
    }

    void SpawnWave(float angle, float power)
    {
        if (waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is not assigned!");
            return;
        }

        currentWave = waveSpawner.SpawnWave(angle, power);

        if (currentWave != null)
        {
            float lifetime = waveSpawner.GetWaveLifetime();
            Invoke(nameof(OnWaveLifetimeEnded), lifetime + 1f);
        }
    }

    void OnWaveLifetimeEnded()
    {
        WaveGameEvents.InvokeWaveDestroyed();

        if (waveSpawner != null && waveSpawner.cameraFollow != null)
        {
            waveSpawner.cameraFollow.ReturnToOriginalPosition();
        }

        StartNewCycle();
    }

    void StartNewCycle()
    {
        if (directionSelector != null)
        {
            directionSelector.Activate();
            currentState = GameState.SelectingDirection;
        }
    }
}
