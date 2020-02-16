using UnityEngine;

public class BreakingEffectController : MonoBehaviour
{
    [SerializeField] private Sprite[] breakingAnim;

    [SerializeField] private InnerWallController innerWallController;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        var spriteToUse = 
            Mathf.FloorToInt(
            Mathf.Clamp(
                innerWallController.GetBreakingPercent() * breakingAnim.Length, 0, breakingAnim.Length - 1));
        spriteRenderer.sprite = breakingAnim[spriteToUse];
    }
}
