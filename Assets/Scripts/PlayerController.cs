using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private GameObject breakingFX;

    public bool canMove = true;
    private GameObject nextLevelImage;
    private GameObject gameOverImage;
    private CameraController cameraController;
    [SerializeField] private Animator anim;
    private static readonly int IsMining = Animator.StringToHash("IsMining");
    [SerializeField] private GameObject visual;

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        cameraController = Camera.main.GetComponent<CameraController>();
        nextLevelImage = GameObject.Find("NextLevelImage");
        gameOverImage = GameObject.Find("GameOverImage");
        if (nextLevelImage == null)
        {
            Debug.LogError("CANVAS NOT ACTIVATED. PLEASE ACTIVATE BEFORE BUILDING GAME.");
            return;
        }
        nextLevelImage.SetActive(false);
        gameOverImage.SetActive(false);
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
            visual.transform.localScale = Vector3.one;
      
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveVec  = Vector2.left;
            visual.transform.localScale = Vector3.one + Vector3.left * 2;
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
            cameraController.DoScreenShake();
            MoveAndDrag(moveVec);
            Instantiate(breakingFX, transform.position + Vector3.back * .1f, Quaternion.identity);
            anim.SetTrigger(IsMining);
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
            StartCoroutine(nameof(DownTheHole));
        }
    }

    private IEnumerator DownTheHole()
    {
        transform.DOMove(transform.position + Vector3.up * .25f, .33f);
        yield return transform.DOScale(Vector3.one * 1.25f, .33f).WaitForCompletion();
        transform.DOScale(0, 1f);
        transform.DOMove(transform.position - Vector3.up * .5f, 1f);
        nextLevelImage.SetActive(true);
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