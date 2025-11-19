using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave Settings")]
    public float lifetime = 10f;
    public float pushForce = 500f;
    
    [Header("Visual Effects")]
    public ParticleSystem splashEffect;
    
    [Header("Height Animation")]
    public bool animateHeight = true;
    public float waveHeightMultiplier = 0.3f;
    public float waveFrequency = 2f;
    
    private Vector3 direction;
    private float speed;
    private float spawnTime;
    private Rigidbody rb;
    private Vector3 startScale;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        startScale = transform.localScale;
    }
    
    public void Initialize(Vector3 waveDirection, float waveSpeed)
    {
        direction = waveDirection.normalized;
        speed = waveSpeed;
        spawnTime = Time.time;
        
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
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
            Vector3 movement = direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if (otherRb != null && !otherRb.isKinematic)
        {
            Vector3 pushDirection = (other.transform.position - transform.position).normalized;
            pushDirection.y = 0.5f;
            otherRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
        
        if (splashEffect != null)
        {
            ParticleSystem splash = Instantiate(splashEffect, other.transform.position, Quaternion.identity);
            Destroy(splash.gameObject, 2f);
        }
    }
}
