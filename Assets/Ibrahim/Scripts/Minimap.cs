using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    private void LateUpdate()
    {
        Vector3 newPosition = playerPos.position; // var for the player position
        newPosition.y = transform.position.y; // we keep the camera distance always the same
        transform.position = newPosition; // change the camera position with the player
    }
}
