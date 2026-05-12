using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public int[] correctOrder;

    private int currentIndex = 0;

    public GameObject door;

    void Awake()
    {
        instance = this;
    }

    public void CheckTile(int number)
    {
        // إذا الرقم صح
        if (number == correctOrder[currentIndex])
        {
            Debug.Log("Correct!");

            currentIndex++;

            // خلص اللغز
            if (currentIndex >= correctOrder.Length)
            {
                Debug.Log("Puzzle Solved!");

                OpenDoor();
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
        door.SetActive(false);
    }
}