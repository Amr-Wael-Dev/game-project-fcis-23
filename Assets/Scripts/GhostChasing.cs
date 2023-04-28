using UnityEngine;

public class GhostChasing : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D collider) {
        Node node = collider.GetComponent<Node>();

        if (node != null && enabled && !ghost.ghostFrightened.enabled) {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections) {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, transform.position.z);
                float distance = (ghost.pacman.position - newPosition).sqrMagnitude;

                if (distance < minDistance) {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void OnDisable() {
        ghost.ghostWandering.Enable();
    }
}
