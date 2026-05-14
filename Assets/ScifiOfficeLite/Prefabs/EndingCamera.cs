using UnityEngine;

public class EndingCamera : MonoBehaviour
{
    public float moveSpeed = 14f;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}