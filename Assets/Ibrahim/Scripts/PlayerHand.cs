using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Transform handPoint;

    GameObject currentItem;

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
        item.GetComponent<Collider>().enabled = false;

        Debug.Log("Picked up: " + item.name);
    }
}