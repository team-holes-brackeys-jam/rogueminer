using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField] private int health = 4;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinReward = 2;
    
    public void Hurt()
    {
        --health;
        if (health > 0) return;

        var rigidbody2DList = new List<Rigidbody2D>();
        for (int i = 0; i < coinReward; i++)
        {
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            rigidbody2DList.Add(coin.GetComponent<Rigidbody2D>());
        }

        foreach (var rigidbody2D in rigidbody2DList)
        {
            rigidbody2D.AddForce(Random.rotation * Vector2.one, ForceMode2D.Impulse);
        }
        
        Destroy(gameObject);
    }
}