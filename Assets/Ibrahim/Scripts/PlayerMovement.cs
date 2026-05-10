using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController cc;

    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float speed;
    [SerializeField] float speedMulti = 1.8f;
    [SerializeField] float jumpHeight = 5f;


    public int gemsCollected = 0;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        speed = walkSpeed;
    }
    void Update()
    {
        isGrounded = cc.isGrounded;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && gemsCollected >= 1)
        {
                speed = walkSpeed * speedMulti;
            
        }
        else
        {
            speed = walkSpeed;
        }
        
        if (velocity.y < 0 && isGrounded)
        {
            velocity.y = -2;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && gemsCollected >= 2)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        cc.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }
}