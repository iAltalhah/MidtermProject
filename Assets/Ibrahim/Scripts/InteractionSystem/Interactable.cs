using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string interactablePrompt = "Interact";
    [SerializeField] bool isEnabled = true;
    [SerializeField] UnityEvent onInteract;

    public string InteractionPrompt => interactablePrompt;

    public bool CanInteract()
    {
        return isEnabled;
    }

    public void Interact()
    {
        if (!CanInteract())
            return;

        onInteract?.Invoke();
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value;
    }
}