using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{

    public bool canMove = true;
    [SerializeField] private GameObject nextLevelImage;
    [SerializeField] private GameObject gameOverImage;
    public Animator anim;  
    
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
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveVec = Vector2.right;
      
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveVec  = Vector2.left;
          
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveVec = Vector2.up;
         
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveVec  = Vector2.down;
   
        }

        var hit = Physics2D.Raycast(transform.position, moveVec, 1f);
        if (hit.collider == null)
        {
            MoveAndDrag(moveVec);
        }
        else if(hit.collider.CompareTag("InnerWall"))
        {
            Destroy(hit.collider.gameObject);
            MoveAndDrag(moveVec);

            anim.SetTrigger("IsMining"); 
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
        else if(hit.collider.CompareTag("Goal"))
        {
            MoveAndDrag(moveVec);
            canMove = false;
            nextLevelImage.SetActive(true);
        }
    }

    private void MoveAndDrag(Vector2 moveVec)
    {
        var dragged = CheckDrag(moveVec);
        transform.position = (Vector2) transform.position + moveVec;
        EndDrag(dragged, moveVec);
    }

    private void EndDrag(GameObject dragged, Vector2 moveVec)
    {
        if (dragged == null)
        {
            return;
        }
        dragged.transform.position = (Vector2) transform.position - moveVec;
    }

    private GameObject CheckDrag(Vector2 moveVec)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 dragVec = moveVec * -1;
            var dragHit = Physics2D.Raycast(transform.position, dragVec, 1f);
            if (dragHit.collider != null && dragHit.collider.CompareTag("Turret"))
            {
                return dragHit.collider.gameObject;
            }
        }

        return null;
    }

    private void OnDestroy()
    {
        if (gameOverImage == null)
        {
            return;
        }
        gameOverImage.SetActive(true);
    }
}