using System.Collections;
using UnityEngine;
using UnityEngine.LowLevel;

public class PadlockPuzzle : MonoBehaviour
{
    [Header("Puzzle Camera Point")]
    public Transform puzzleCameraPoint;

    [Header("Camera Reference")]
    public Camera playerCamera;

    [Header("Settings")]
    public float moveSpeed = 6f;

    // حفظ مكان الكاميرا الأصلي
    private Vector3 originalPos;
    private Quaternion originalRot;

    // سكربتات اللاعب
    private PlayerMovement movement;
    private MouseLook look;
 

    // سكربت القفل
    private MoveRuller moveRuller;

    private bool inPuzzle = false;

    void Start()
    {
        // يجيب اللاعب
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        movement = player.GetComponent<PlayerMovement>();
        look = player.GetComponent<MouseLook>();
     

        // يجيب سكربت القفل
        moveRuller = GetComponent<MoveRuller>();
    }

    public  void Interact()
    {
        if (!inPuzzle)
        {
            StartCoroutine(EnterPuzzle());
        }
    }

    IEnumerator EnterPuzzle()
    {
        inPuzzle = true;

        // حفظ مكان الكاميرا
        originalPos = playerCamera.transform.position;
        originalRot = playerCamera.transform.rotation;

        // إيقاف اللاعب
        movement.canMove = false;

        look.enabled = false;
       

        // تشغيل تحكم القفل
        moveRuller.canRotate = true;

        // إظهار الماوس
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // تحريك الكاميرا للقفل
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

        // تثبيت الكاميرا
        playerCamera.transform.position = puzzleCameraPoint.position;
        playerCamera.transform.rotation = puzzleCameraPoint.rotation;
    }

    void Update()
    {
        // خروج من البزل
        if (inPuzzle && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitPuzzle());
        }
    }

    IEnumerator ExitPuzzle()
    {
        // رجوع الكاميرا
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

        // تثبيت الكاميرا
        playerCamera.transform.position = originalPos;
        playerCamera.transform.rotation = originalRot;

        // تشغيل اللاعب
        movement.canMove = true;

        look.enabled = true;
    

        // إيقاف تحكم القفل
        moveRuller.canRotate = false;

        // إخفاء الماوس
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inPuzzle = false;
    }
}