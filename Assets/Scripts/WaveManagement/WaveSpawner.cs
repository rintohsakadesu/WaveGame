using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject wavePrefab;
    public CameraFollow cameraFollow;
    
    [Header("Wave Settings")]
    public float waveLifetime = 5f;
    
    public GameObject SpawnWave(float angle, float power)
    {
        if (wavePrefab == null)
        {
            Debug.LogError("Wave prefab is not assigned!");
            return null;
        }
        
        Quaternion rotation = transform.rotation * Quaternion.Euler(0f, angle, 0f);
        GameObject wave = Instantiate(wavePrefab, transform.position, rotation);
        
        WaveController waveController = wave.GetComponent<WaveController>();
        if (waveController != null)
        {
            waveController.Initialize(power);
        }
        
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(wave.transform);
        }
        
        return wave;
    }
    
    public float GetWaveLifetime()
    {
        return waveLifetime;
    }
}
