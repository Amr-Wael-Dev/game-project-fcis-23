using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.1f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(AdvanceAnimation), animationTime, animationTime);
    }

    private void AdvanceAnimation() {
        if (!spriteRenderer.enabled) {
            return;
        }

        animationFrame++;
        if (loop && animationFrame >= sprites.Length) {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    private void RestartAnimation() {
        animationFrame = -1;

        AdvanceAnimation();
    }
}
