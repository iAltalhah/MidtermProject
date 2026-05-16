using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public int[] correctOrder;
    private int currentIndex = 0;

    public Animator door3Anim;

    [Header("Puzzle Tiles")]
    [SerializeField] private Transform tilesParent;
    private Collider[] tileColliders;

    [SerializeField] AudioSource doorSound;
    [SerializeField] AudioSource wrongSound;
    [SerializeField] AudioSource correct;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tileColliders = tilesParent.GetComponentsInChildren<Collider>();
    }

    public void CheckTile(int number)
    {
        if (number == correctOrder[currentIndex])
        {
            Debug.Log("Correct!");
            correct.Play();

            currentIndex++;

            if (currentIndex >= correctOrder.Length)
            {
                Debug.Log("Puzzle Solved!");

                OpenDoor();
                DisableAllTileCollisions();
            }
        }
        else
        {
            Debug.Log("Wrong Order!");
            wrongSound.Play();
            currentIndex = 0;
        }
    }

    void OpenDoor()
    {
        door3Anim.Play("door3Open");
        doorSound.Play();
    }

    void DisableAllTileCollisions()
    {
        foreach (Collider col in tileColliders)
        {
            col.enabled = false;
        }
    }
}