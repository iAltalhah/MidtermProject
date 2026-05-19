using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string interactablePrompt = "Interact";
    [SerializeField] bool isEnabled = true;
    [SerializeField] UnityEvent onInteract;

    public string InteractionPrompt => interactablePrompt;


    public void Interact()
    {

        onInteract?.Invoke();
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value;
    }
}