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
    [SerializeField] float footstepGroundGraceTime = 0.15f;
    [SerializeField] float minimumFootstepGap = 0.08f;

    float lastGroundedTime;
    float lastFootstepTime;
    public bool canMove = true;
    public int gemsCollected = 0;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    float footstepTimer;
    bool isRunning;
    bool wasMoving;
    bool wasRunningLastFrame;

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
            ResetFootsteps();
            return;
        }

        isGrounded = cc.isGrounded;

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded;

        if (isRunning)
            speed = walkSpeed * speedMulti;
        else
            speed = walkSpeed;

        if (velocity.y < 0 && isGrounded)
            velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gemsCollected >= 1)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        Vector3 totalMove = (move * speed + velocity) * Time.deltaTime;
        cc.Move(totalMove);

        HandleFootsteps(x, z);
    }

    private void HandleFootsteps(float x, float z)
    {
        bool hasMovementInput = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;

        // This prevents tiny bumps/slopes from breaking the footstep logic
        bool recentlyGrounded = isGrounded || Time.time - lastGroundedTime <= footstepGroundGraceTime;

        if (!hasMovementInput)
        {
            ResetFootsteps();
            return;
        }

        // If the player is really in the air, do not play footsteps,
        // but also do not reset wasMoving immediately.
        if (!recentlyGrounded)
        {
            return;
        }

        // First step happens immediately when player starts moving
        if (!wasMoving)
        {
            PlayRandomFootstep();
            footstepTimer = isRunning ? runStepInterval : walkStepInterval;

            wasMoving = true;
            wasRunningLastFrame = isRunning;
            return;
        }

        // When switching between walking and running, reset timing
        if (isRunning != wasRunningLastFrame)
        {
            footstepTimer = 0f;
            wasRunningLastFrame = isRunning;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            PlayRandomFootstep();
            footstepTimer = isRunning ? runStepInterval : walkStepInterval;
        }
    }

    private void ResetFootsteps()
    {
        footstepTimer = 0.02f;
        wasMoving = false;
        wasRunningLastFrame = false;
    }

    private void PlayRandomFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || footstepAudioSource == null)
            return;

        // Prevents tiny repeated double-steps
        if (Time.time - lastFootstepTime < minimumFootstepGap)
            return;

        lastFootstepTime = Time.time;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepAudioSource.PlayOneShot(clip);
    }
}