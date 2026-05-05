using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    [SerializeField] Transform interactionSource;
    [SerializeField] float interactionRange;
    [SerializeField] float interactionTime;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        if(Physics.Raycast(ray,out RaycastHit hit, interactionRange))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
