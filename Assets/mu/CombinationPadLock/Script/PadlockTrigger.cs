using UnityEngine;
using System.Collections;

public class PadlockTrigger : MonoBehaviour
{
    [Header("Camera Point")]
    public Transform puzzleCameraPoint;

    [Header("References")]
    public Camera playerCamera;
    public PlayerMovement movement;
    public MouseLook mouseLook;
    public MoveRuller moveRuller;

    [Header("Settings")]
    public float interactDistance = 3f;
    public float moveSpeed = 5f;

    private Vector3 originalPos;
    private Quaternion originalRot;

    private bool inPuzzle = false;
    private bool playerNear = false;

    void Update()
    {
        // دخول للقفل
        if (playerNear && !inPuzzle && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterPuzzle());
        }

        // خروج من القفل
        if (inPuzzle && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitPuzzle());
        }
    }

    IEnumerator EnterPuzzle()
    {
        inPuzzle = true;

        originalPos = playerCamera.transform.position;
        originalRot = playerCamera.transform.rotation;

        movement.enabled = false;
        mouseLook.enabled = false;

        moveRuller.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        while (Vector3.Distance(playerCamera.transform.position, puzzleCameraPoint.position) > 0.01f)
        {
            playerCamera.transform.position = Vector3.Lerp(
                playerCamera.transform.position,
                puzzleCameraPoint.position,
                Time.deltaTime * moveSpeed
            );

            playerCamera.transform.rotation = Quaternion.Lerp(
                playerCamera.transform.rotation,
                puzzleCameraPoint.rotation,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }
    }

    IEnumerator ExitPuzzle()
    {
        while (Vector3.Distance(playerCamera.transform.position, originalPos) > 0.01f)
        {
            playerCamera.transform.position = Vector3.Lerp(
                playerCamera.transform.position,
                originalPos,
                Time.deltaTime * moveSpeed
            );

            playerCamera.transform.rotation = Quaternion.Lerp(
                playerCamera.transform.rotation,
                originalRot,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }

        movement.enabled = true;
        mouseLook.enabled = true;

        moveRuller.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inPuzzle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}