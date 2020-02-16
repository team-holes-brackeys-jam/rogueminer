using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _bulletSpeed;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _destination;

    public void Init(float bulletSpeed, Vector3 destination)
    {
        _bulletSpeed = bulletSpeed;
        _destination = destination;

    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Vector2 targetPos;
        targetPos.x = _destination.x - transform.position.x;
        targetPos.y = _destination.y - transform.position.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(transform.position + transform.up * (Time.deltaTime * _bulletSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall"))
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyController>().Hurt();
            }
            Destroy(gameObject);
        }
            
    }
}
