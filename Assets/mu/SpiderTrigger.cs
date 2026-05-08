using UnityEngine;

public class SpiderTrigger : MonoBehaviour
{
    public SpiderAI spider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spider.StartDrop();

            Debug.Log("Spider Activated!");
        }
    }
}