using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController cc;

    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float speed;
    [SerializeField] float speedMulti = 1.8f;
    [SerializeField] float jumpHeight = 5f;

    [SerializeField] private GameObject objectToToggle;

    [Header("Footsteps")]
    [SerializeField] AudioSource footstepAudioSource;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] float walkStepInterval = 0.45f;
    [SerializeField] float runStepInterval = 0.25f;

    public bool canMove = true;
    public int gemsCollected = 0;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    float footstepTimer;
    bool isRunning;

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

        isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded;

        if (isRunning) speed = walkSpeed * speedMulti;
        else speed = walkSpeed;


        if (velocity.y < 0 && isGrounded)
            velocity.y = -2f;

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gemsCollected >= 1)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            velocity.y += gravity * Time.deltaTime;

            Vector3 totalMove = (move * speed + velocity) * Time.deltaTime;
            cc.Move(totalMove);
        }

        HandleFootsteps(x, z);
    }

    /// <summary>
    /// Plays footstep audio at a regular interval when the player is grounded and moving.
    /// </summary>
    private void HandleFootsteps(float x, float z)
    {
        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f) && isGrounded;

        if (!isMoving)
        {
            footstepTimer = 0.1f;
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            PlayRandomFootstep();
            footstepTimer = isRunning ? runStepInterval : walkStepInterval;
        }
    }

    private void PlayRandomFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || footstepAudioSource == null)
            return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepAudioSource.PlayOneShot(clip);
    }
}
