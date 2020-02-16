using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField] private int health = 4;

    public void Hurt()
    {
        --health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}