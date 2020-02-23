using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Vector3 originalPos;

    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeIntensity = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

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