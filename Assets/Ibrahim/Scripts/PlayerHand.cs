using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Transform handPoint;
    [SerializeField] DropItemsOff dropItemsOff;

    GameObject currentItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReturnHeldItem();
        }
    }

    public bool IsFull()
    {
        return currentItem != null;
    }

    public void HoldItem(GameObject item)
    {
        if (IsFull())
        {
            Debug.Log("Hand is already full.");
            return;
        }

        currentItem = item;

        item.transform.SetParent(handPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        Collider itemCollider = item.GetComponent<Collider>();
        if (itemCollider != null)
            itemCollider.enabled = false;

        Debug.Log("Picked up: " + item.name);
    }

    public void DropItem()
    {
        if (currentItem == null)
        {
            Debug.Log("You are not holding anything.");
            return;
        }

        bool itemWasPlaced = dropItemsOff.CheckTheItem(currentItem);

        if (itemWasPlaced)
        {
            currentItem = null;
        }
    }
    public void ReturnHeldItem()
    {
        if (currentItem == null)
        {
            Debug.Log("No item in hand.");
            return;
        }

        ItemPickUp itemPickUp = currentItem.GetComponent<ItemPickUp>();

        if (itemPickUp != null)
        {
            itemPickUp.ReturnToOriginalPosition();
        }

        currentItem = null;
    }

    public GameObject GetCurrentItem()
    {
        return currentItem;
    }

    public void ClearCurrentItem()
    {
        currentItem = null;
    }

}