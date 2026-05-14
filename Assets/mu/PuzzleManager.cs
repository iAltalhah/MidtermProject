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

            currentIndex = 0;
        }
    }

    void OpenDoor()
    {
        door3Anim.Play("door3Open");
    }

    void DisableAllTileCollisions()
    {
        foreach (Collider col in tileColliders)
        {
            col.enabled = false;
        }
    }
}