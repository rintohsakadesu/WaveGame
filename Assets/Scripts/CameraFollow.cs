using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Vector3 baseOffset = new Vector3(0f, 8f, -10f);
    public float followSpeed = 10f;
    public bool lookAtTarget = true;
    
    [Header("Dynamic Settings")]
    public bool useSpeedBasedOffset = true;
    public float speedOffsetMultiplier = 0.5f;
    
    [Header("Look At Settings")]
    public Vector3 lookAtOffset = new Vector3(0f, 0f, 2f);
    public float rotationSpeed = 8f;
    
    [Header("Return Settings")]
    public float returnSpeed = 5f;
    
    private Transform targetWave;
    private Vector3 velocity = Vector3.zero;
    private Vector3 previousPosition;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool returningToStart = false;
    
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    
    public void SetTarget(Transform newTarget)
    {
        targetWave = newTarget;
        returningToStart = false;
        
        if (targetWave != null)
        {
            previousPosition = targetWave.position;
        }
    }
    
    public void ReturnToOriginalPosition()
    {
        targetWave = null;
        returningToStart = true;
    }
    
    void LateUpdate()
    {
        if (returningToStart)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, returnSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                returningToStart = false;
            }
            return;
        }
        
        if (targetWave == null) return;
        
        Vector3 targetPosition = targetWave.position;
        Vector3 currentOffset = baseOffset;
        
        if (useSpeedBasedOffset)
        {
            Vector3 waveVelocity = (targetPosition - previousPosition) / Time.deltaTime;
            currentOffset.z -= waveVelocity.magnitude * speedOffsetMultiplier;
        }
        
        Vector3 desiredPosition = targetPosition + targetWave.TransformDirection(currentOffset);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);
        
        if (lookAtTarget)
        {
            Vector3 lookAtPosition = targetPosition + targetWave.TransformDirection(lookAtOffset);
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        previousPosition = targetPosition;
    }
}
