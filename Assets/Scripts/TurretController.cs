using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject breakFX;
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }
    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, transform.rotation * Vector2.up);
        if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Turret") ||
            hit.collider.gameObject == gameObject)
        {
            return;
        }
        PlayerController.Instance.canMove = false;
        Instantiate(arrowPrefab, transform.position, transform.rotation);
        Instantiate(breakFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public RaycastHit2D CheckCollisions(Vector2 direction)
    {
        var hit = Physics2D.Raycast(transform.position, direction, 1f);
        return hit;
    }
}
