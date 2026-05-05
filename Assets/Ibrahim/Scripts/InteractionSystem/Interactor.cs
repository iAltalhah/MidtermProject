using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Transform interactionSource;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] KeyCode interactionKey = KeyCode.E; // maybe we can change it
    [SerializeField] InteractionPromptUI promptUI;

    IInteractable currentInteractable; // to check which interactable is it

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward); // create raycast and its range

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange)) // hit is the object we are hitting with the ray
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>(); // we define a new var and assign the interactable conponent to it

            if (interactable != null && interactable.CanInteract())
            {
                if (interactable != currentInteractable)
                {
                    currentInteractable = interactable;
                    promptUI.ShowPrompt(interactable.InteractionPrompt);
                }

                return;
            }
        }

        if (currentInteractable != null) // 
        {
            currentInteractable = null;
            promptUI.HidePrompt();
        }
    }
}