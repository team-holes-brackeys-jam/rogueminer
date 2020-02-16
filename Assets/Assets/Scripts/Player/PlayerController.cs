using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField]
    private float speed = 6;

    [SerializeField] private float miningSpeed = 25f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private bool isGodMode;
    private float _secondsWhileCurrentAttack;
    
    private Rigidbody2D _rigidbody2D;
    private List<Transform> _enemies;

    [SerializeField]
    private LayerMask layerMask;

    private GameObject _charactersPivot;

    [SerializeField]
    private float bulletSpeed = 10f;

    private void Awake()
    {
        if (isGodMode)
        {
            attackSpeed = .2f;
            speed = 20;
        }
        _secondsWhileCurrentAttack = attackSpeed;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _charactersPivot = GameObject.Find("Characters");
    }

    private void Update()
    {
        if (_secondsWhileCurrentAttack < attackSpeed)
        {
            _secondsWhileCurrentAttack += Time.deltaTime;
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (!other.gameObject.CompareTag("InnerWall"))
        {
            return;
        }

        var controller = other.gameObject.GetComponent<InnerWallController>();
        controller.currentDurability -= miningSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        PassiveShot();
    }
    
    private void PassiveShot()
    {
        _enemies = _charactersPivot.GetComponentsInChildren<EnemyController>().Select(o => o.transform).ToList();
        var inputValue = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody2D.MovePosition(transform.position + (Vector3) inputValue * (Time.deltaTime * speed));

        var closestDist = float.MaxValue;
        var closestEnemy = GetClosestEnemy(closestDist);

        if (closestEnemy == null) return;

        closestEnemy.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        Vector2 playerEyes = (Vector2) transform.position + Vector2.one * .5f;
        Vector2 enemyEyes = (Vector2) closestEnemy.position + Vector2.one * .5f;
        RaycastHit2D hit = Physics2D.Raycast(playerEyes, (enemyEyes - playerEyes).normalized, float.MaxValue, layerMask);
        if (hit.collider != null)
        {
            Debug.DrawLine((Vector2) transform.position + Vector2.one * .5f, hit.point, Color.black);
            // Debug.DrawLine((Vector2)transform.position + Vector2.one * .5f, (Vector2)closestEnemy.position + Vector2.one * .5f, Color.black);
            if (hit.collider.GetComponent<EnemyController>() == null)
            {
                closestEnemy.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else
            {
                ShootTowards(enemyEyes, playerEyes);
            }
        }
        else
        {
            Debug.DrawLine((Vector2) transform.position + Vector2.one * .5f,
                (Vector2) closestEnemy.position + Vector2.one * .5f, Color.black);
        }
    }

    private void ShootTowards(Vector3 closestEnemyPosition, Vector3 playerEyes)
    {
        if (_secondsWhileCurrentAttack >= attackSpeed)
        {
            _secondsWhileCurrentAttack = 0;
            var go = Instantiate(bulletPrefab, playerEyes, Quaternion.identity);
            go.GetComponent<BulletController>().Init(bulletSpeed, closestEnemyPosition);
        }
    }

    private Transform GetClosestEnemy(float closestDist)
    {
        Transform closestEnemy = null;
        foreach (var enemy in _enemies)
        {
            var dist = Vector2.Distance(transform.position, enemy.position);
            if (dist < closestDist)
            {
                closestEnemy = enemy;
                closestDist = dist;
            }

            enemy.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        }

        return closestEnemy;
    }
}