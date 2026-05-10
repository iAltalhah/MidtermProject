using UnityEngine;

public class TimerStop : MonoBehaviour
{
    [SerializeField] GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        manager.isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        manager.isPlayerInside = false;
        
    }
}
