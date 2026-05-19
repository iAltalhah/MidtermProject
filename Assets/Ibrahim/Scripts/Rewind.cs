using System.Collections;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [SerializeField] Transform rewindAnchor;
    [SerializeField] KeyCode rewindKey = KeyCode.Mouse1;

    [SerializeField] float rewindDuration = 0.5f;

    [SerializeField] AudioSource rewaindSound;
    [SerializeField] Animator rewindAnim;

    CharacterController characterController;
    PlayerMovement playerMovement;

    bool canRewind = false;
    int gemsCount;

    bool isRewinding;

    void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if (Input.GetKeyDown(rewindKey) && !isRewinding && canRewind)
        {
            rewaindSound.Play();
            rewindAnim.Play("rewindAnim", 0, 0f);
            StartCoroutine(RewindToAnchor());
        }
    }

    IEnumerator RewindToAnchor()
    {
        if (rewindAnchor == null)
            yield break;

        isRewinding = true;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (characterController != null)
            characterController.enabled = false;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = rewindAnchor.position;

        float elapsedTime = 0f;

        while (elapsedTime < rewindDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / rewindDuration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;

        if (characterController != null)
            characterController.enabled = true;

        if (playerMovement != null)
            playerMovement.enabled = true;

        isRewinding = false;
    }

    public void CanRewind()
    {
         canRewind = true;
    }
}