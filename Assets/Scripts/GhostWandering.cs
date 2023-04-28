using UnityEngine;

public class GhostWandering : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D collider) {
        Node node = collider.GetComponent<Node>();

        if (node != null && enabled && !ghost.ghostFrightened.enabled) {
            int random = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[random] == -ghost.movement.direction && node.availableDirections.Count > 1) {
                random++;
                if (random >= node.availableDirections.Count) {
                    random = 0;
                }
            }

            ghost.movement.SetDirection(node.availableDirections[random]);
        }
    }

    private void OnDisable() {
        ghost.ghostChasing.Enable();
    }
}
