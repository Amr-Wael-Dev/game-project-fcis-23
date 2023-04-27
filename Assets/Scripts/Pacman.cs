using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }

    private void Awake() {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }

        float rotationAngle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(rotationAngle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState() {
        gameObject.SetActive(true);
        movement.ResetState();
    }
}
