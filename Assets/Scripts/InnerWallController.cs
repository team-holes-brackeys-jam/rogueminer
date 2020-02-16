using UnityEngine;

public class InnerWallController : MonoBehaviour
{
    [SerializeField]
    private float maxDurability = 100;
    public float currentDurability;

    private void Awake()
    {
        currentDurability = maxDurability;
    }

    private void Update()
    {
        if (currentDurability <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetBreakingPercent()
    {
        return 1 - currentDurability / maxDurability;
    }
}
