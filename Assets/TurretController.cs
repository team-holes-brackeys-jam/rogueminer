using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }
    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, transform.rotation * Vector2.up);
        if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Turret")) return;
        PlayerController.Instance.canMove = false;
        StartCoroutine(nameof(Shoot), hit.collider.gameObject);
    }

    private IEnumerator Shoot(GameObject value)
    {
        while (Vector3.Distance(transform.position, value.transform.position) > .25f)
        {

            transform.position = Vector3.MoveTowards(
                transform.position,
                value.transform.position,
                speed * Time.deltaTime);
            yield return null;
        }
        Destroy(value);
        Destroy(gameObject);

        PlayerController.Instance.canMove = true;
    }

    public RaycastHit2D CheckCollisions(Vector2 direction)
    {
        var hit = Physics2D.Raycast(transform.position, direction, 1f);
        return hit;
    }
}
