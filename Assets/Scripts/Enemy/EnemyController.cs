using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : EntityController
{
    [SerializeField] private int health = 4;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinReward = 2;

    public void Hurt()
    {
        
        --health;
        Camera.main.GetComponent<CameraController>().DoPulseBloom();
        
        if (health > 0) return;

        var rigidbody2DList = new List<Rigidbody2D>();
        for (int i = 0; i < coinReward; i++)
        {
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            rigidbody2DList.Add(coin.GetComponent<Rigidbody2D>());
        }

        foreach (var rb in rigidbody2DList)
        {
            rb.AddForce(Random.rotation * Vector2.one, ForceMode2D.Impulse);
        }
        
        Destroy(gameObject);
    }


}