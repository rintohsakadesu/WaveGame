using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave Settings")]
    public float lifetime = 10f;
    public float basePushForce = 500f;
    
    [Header("Visual Effects")]
    public ParticleSystem splashEffect;
    public bool continuousSplash = true;
    public Transform waveParticleOrigin;
    
    [Header("Height Animation")]
    public bool animateHeight = true;
    public float waveHeightMultiplier = 0.3f;
    public float waveFrequency = 2f;
    
    private float speed;
    private float currentPushForce;
    private float spawnTime;
    private Rigidbody rb;
    private Vector3 startScale;
    private ParticleSystem activeSplash;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startScale = transform.localScale;
    }
    
    public void Initialize(float power)
    {
        speed = power;
        currentPushForce = basePushForce * (power / 10f);
        spawnTime = Time.time;
        
        if (continuousSplash && splashEffect != null && waveParticleOrigin != null)
        {
            activeSplash = Instantiate(splashEffect, waveParticleOrigin.position, waveParticleOrigin.rotation);
            activeSplash.transform.SetParent(waveParticleOrigin);
            activeSplash.transform.localPosition = Vector3.zero;
            activeSplash.transform.localRotation = Quaternion.identity;
            
            if (!activeSplash.isPlaying)
            {
                activeSplash.Play();
            }
        }
        else
        {
            if (!continuousSplash)
                Debug.LogWarning("Continuous Splash is disabled");
            if (splashEffect == null)
                Debug.LogWarning("Splash Effect is not assigned!");
            if (waveParticleOrigin == null)
                Debug.LogWarning("Wave Particle Origin is not assigned!");
        }
    }
    
    void Update()
    {
        if (animateHeight)
        {
            float wave = (Mathf.Sin(Time.time * waveFrequency) + 1f) * 0.5f;
            float heightVariation = Mathf.Lerp(1f - waveHeightMultiplier, 1f + waveHeightMultiplier, wave);
            
            Vector3 newScale = startScale;
            newScale.y = startScale.y * heightVariation;
            transform.localScale = newScale;
        }
        
        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;
        if (otherRb != null && !otherRb.isKinematic)
        {
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            pushDirection.y = 0.5f;
            otherRb.AddForce(pushDirection * currentPushForce, ForceMode.Impulse);
        }
    }
    
    void OnDestroy()
    {
        if (activeSplash != null)
        {
            activeSplash.Stop();
            Destroy(activeSplash.gameObject, 2f);
        }
    }
}
