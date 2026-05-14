using UnityEngine;

public class SpiderTrigger : MonoBehaviour
{
    public SpiderAI spider;
    bool isCalledMethod = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCalledMethod = true;  
            spider.StartDrop();

            Debug.Log("Spider Activated!");
        }
    }
}