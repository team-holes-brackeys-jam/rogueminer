using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController.Instance.canMove = true;
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Ouch();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
