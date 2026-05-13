using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] PlayerHand playerHand;
    [SerializeField] Animator animator;

    Transform originalParent;
    Vector3 originalPosition;
    Quaternion originalRotation;

    bool isPickedUp;

    private void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void PickUp()
    {
        if (playerHand.IsFull())
        {
            Debug.Log("Cannot pick up. Hand is full.");
            return;
        }

        playerHand.HoldItem(gameObject);
        isPickedUp = true;

        if (gameObject.name == "Gem2")
        {
            animator.Play("closeDooe1");
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Collider itemCollider = GetComponent<Collider>();

        if (itemCollider != null)
            itemCollider.enabled = true;

        isPickedUp = false;

        if (gameObject.name == "Gem2")
        {
            animator.Play("door1Open");
        }

        Debug.Log("Returned item to original position: " + gameObject.name);
    }
}