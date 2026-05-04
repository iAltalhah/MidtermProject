using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float speed;
    [SerializeField] float speedMulti = 1.8f;
    
    float gravity = -9.81f;
    Vector3 velocity;

    private void Start()
    {
        speed = walkSpeed;
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed * speedMulti;
        }
        else
        {
            speed = walkSpeed;
        }

            controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}