using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostAtHome ghostAtHome { get; private set; }
    public GhostChasing ghostChasing { get; private set; }
    public GhostFrightened ghostFrightened { get; private set; }
    public GhostWandering ghostWandering { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform pacman;
    public int points = 200;

    private void Awake() {
        movement = GetComponent<Movement>();
        ghostAtHome = GetComponent<GhostAtHome>();
        ghostChasing = GetComponent<GhostChasing>();
        ghostFrightened = GetComponent<GhostFrightened>();
        ghostWandering = GetComponent<GhostWandering>();
    }

    private void Start() {
        ResetState();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            if (ghostFrightened.enabled) {
                FindObjectOfType<GameManager>().GhostEaten(this);
            } else {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    public void ResetState() {
        gameObject.SetActive(true);
        movement.ResetState();

        ghostFrightened.Disable();
        ghostChasing.Disable();
        ghostWandering.Enable();
        if (ghostAtHome != initialBehavior) {
            ghostAtHome.Disable();
        }

        if (initialBehavior != null) {
            initialBehavior.Enable();
        }
    }
}
