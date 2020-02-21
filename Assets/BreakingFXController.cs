using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingFXController : MonoBehaviour
{
    [SerializeField] private int debriAmmount = 5;
    [SerializeField] private GameObject debriPrefab;

    public void Start()
    {
        var rigidbody2DList = new List<Rigidbody2D>();
        for (var i = 0; i < debriAmmount; i++)
        {
            var coin = Instantiate(debriPrefab, transform.position, Quaternion.identity);
            rigidbody2DList.Add(coin.GetComponent<Rigidbody2D>());
        }

        foreach (var rb in rigidbody2DList)
        {
            rb.AddForce(Random.rotation * Vector2.one, ForceMode2D.Impulse);
        }
        
        Destroy(gameObject);
    }
}
