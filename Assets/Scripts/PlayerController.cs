using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public bool canMove = true;
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove)
        {
            return;
        }
        
        var moveVec = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveVec = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveVec  = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveVec = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveVec  = Vector2.down;
        }

        var hit = Physics2D.Raycast(transform.position, moveVec, 1f);
        if (hit.collider == null)
        {
            CheckDrag(moveVec);
            transform.position = (Vector2)transform.position + moveVec;
        }
        else if(hit.collider.CompareTag("InnerWall"))
        {
            Destroy(hit.collider.gameObject);
            CheckDrag(moveVec);
            transform.position = (Vector2)transform.position + moveVec;
        }
        else if(hit.collider.CompareTag("Turret"))
        {
            TurretController turretController = hit.collider.gameObject.GetComponent<TurretController>();
            var turretHit = turretController.CheckCollisions(moveVec);
            if (turretHit.collider == null)
            {
                turretController.transform.position = (Vector2)turretController.transform.position + moveVec;
                transform.position = (Vector2)transform.position + moveVec;
            }
        }
    }

    private void CheckDrag(Vector2 moveVec)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 dragVec = moveVec * -1;
            var dragHit = Physics2D.Raycast(transform.position, dragVec, 1f);
            if (dragHit.collider != null && dragHit.collider.CompareTag("Turret"))
            {
                dragHit.transform.position = (Vector2) dragHit.transform.position + moveVec;
            }
        }
    }
}