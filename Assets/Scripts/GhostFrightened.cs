using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool eaten { get; private set; }

    private void OnEnable() {
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable() {
        ghost.movement.speedMultiplier = 1.0f;
        eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            if (enabled) {
                Eaten();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Node node = collider.GetComponent<Node>();

        if (node != null && enabled) {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections) {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, transform.position.z);
                float distance = (ghost.pacman.position - newPosition).sqrMagnitude;

                if (distance > maxDistance) {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    public override void Enable(float duration) {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable() {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash() {
        if (!eaten) {
            blue.enabled = false;
            white.enabled = true;

            white.GetComponent<AnimatedSprite>().RestartAnimation();
        }
    }

    private void Eaten() {
        eaten = true;

        Vector3 position = ghost.ghostAtHome.inside.position;
        position.z = ghost.transform.position.z;
        ghost.transform.position = position;

        ghost.ghostAtHome.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
}
