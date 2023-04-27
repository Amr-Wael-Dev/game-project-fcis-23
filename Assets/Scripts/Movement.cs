using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public LayerMask obstacleLayer;
    public Vector2 initialDirection = new Vector2(1.0f, 0.0f);
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startPosition { get; private set; }
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start() {
        ResetState();
    }

    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void Update() {
        if (nextDirection != Vector2.zero) {
            SetDirection(nextDirection);
        }
    }

    public void ResetState() {
        speedMultiplier = 1.0f;
        speed = 8.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    public void SetDirection(Vector2 direction, bool forced = false) {
        if (forced || !DirectionIsOccupied(direction)) {
            this.direction = direction;
            nextDirection = Vector2.zero;
        } else {
            nextDirection = direction;
        }
    }

    public bool DirectionIsOccupied(Vector2 direction) {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}
