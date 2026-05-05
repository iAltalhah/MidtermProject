using UnityEngine;

public class SleepingLogic : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        gameManager.ResetTheDay();
    }
}
