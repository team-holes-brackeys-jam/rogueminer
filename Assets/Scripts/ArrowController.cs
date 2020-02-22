using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, transform.up, .1f);
        if (hit.collider != null)
        {
            PlayerController.Instance.canMove = true;
            Destroy(hit.collider.gameObject);
            Destroy(gameObject);
            
        }
        transform.position += transform.up * (speed * Time.deltaTime);
    }
}
