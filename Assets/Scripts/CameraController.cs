using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField] private float speed = 3;
    private Bloom bloomFx;
    [SerializeField] private float bloomPulseSpeed = 10f;

    private void Start()
    {
        bloomFx = GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>();
        _playerTransform = PlayerController.Instance.transform;
    }

    IEnumerator PulseBloom()
    {
        bloomFx.intensity.value = 30f;
        while (bloomFx.intensity.value > 10f)
        {
            yield return null;
            bloomFx.intensity.value -= Time.deltaTime * bloomPulseSpeed;
        }
        bloomFx.intensity.value = 10f;
    }

    public void DoPulseBloom()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(PulseBloom));
    }
}