using UnityEngine;

public class CollectingMap : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject relatedMap;
    public string InteractionPrompt => throw new System.NotImplementedException();

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        relatedMap.SetActive(false);
        Destroy(gameObject);
    }


}
