using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField] private float speed = 3;
    private Bloom bloomFX;
    [SerializeField] private float bloomPulseSpeed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        bloomFX = Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>();
        _playerTransform = PlayerController.Instance.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_playerTransform == null)
        {
            try
            {
                _playerTransform = PlayerController.Instance.transform;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }
        }
        // TODO Better camera pls
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, speed * Time.deltaTime) + Vector3.forward;
    }
    
    IEnumerator PulseBloom()
    {
        bloomFX.intensity.value = 30f;
        while (bloomFX.intensity.value > 10f)
        {
            Debug.Log(bloomFX.intensity.value);
            yield return null;
            bloomFX.intensity.value -= Time.deltaTime * bloomPulseSpeed;
        }
        bloomFX.intensity.value = 10f;
    }

    public void DoPulseBloom()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(PulseBloom));
    }
}