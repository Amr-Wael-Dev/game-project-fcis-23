using UnityEngine;

public class DarkModeSpotLight : MonoBehaviour
{
    [SerializeField]
    private Transform pacman;

    private void Update()
    {
        if (pacman.gameObject.activeSelf) {
            transform.position = pacman.transform.position;
        }
    }
}
