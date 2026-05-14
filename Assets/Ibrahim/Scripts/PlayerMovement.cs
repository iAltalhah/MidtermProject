using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController cc;

    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float speed;
    [SerializeField] float speedMulti = 1.8f;
    [SerializeField] float jumpHeight = 5f;

    [SerializeField] private GameObject objectToToggle;

    public bool canMove = true;

    public int gemsCollected = 0;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;

    bool isGrounded;

    [Header("Footsteps")]
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private float walkFootstepInterval = 0.5f;

    private float footstepTimer;

    private void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }

        if (!canMove)
        {
            return;
        }

        isGrounded = cc.isGrounded;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Prevent faster diagonal movement
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        bool isMoving = move.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded && isMoving;

        if (isRunning)
        {
            speed = walkSpeed * speedMulti;
        }
        else
        {
            speed = walkSpeed;
        }

        // Gravity
        if (velocity.y < 0 && isGrounded)
        {
            velocity.y = -2;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gemsCollected >= 1)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Player movement
        cc.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        HandleFootsteps(isMoving, isRunning);
    }

    private void HandleFootsteps(bool isMoving, bool isRunning)
    {
        if (!isGrounded || !isMoving)
        {
            footstepTimer = 0.1f;
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            footstepAudioSource.PlayOneShot(footstepClip);

            if (isRunning)
            {
                // Running plays footsteps twice as fast
                footstepTimer = walkFootstepInterval / 2f;
            }
            else
            {
                footstepTimer = walkFootstepInterval;
            }
        }
    }
}