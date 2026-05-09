using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public int tileNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PuzzleManager.instance.CheckTile(tileNumber);
        }
    }
}