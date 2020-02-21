using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 originalPos;

    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeIntensity = 1f;
    
    private void Start()
    {
        originalPos = transform.position;
    }

    public void DoScreenShake()
    {
        transform.position = originalPos;
        transform.DOShakePosition(shakeDuration, Vector3.one * shakeIntensity);
    }
}