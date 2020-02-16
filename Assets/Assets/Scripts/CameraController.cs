using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField] private float speed = 3;
    // Start is called before the first frame update
    private void Start()
    {
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
}