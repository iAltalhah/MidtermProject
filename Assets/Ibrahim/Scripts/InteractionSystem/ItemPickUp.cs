using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] PlayerHand playerHand;

    public void PickUp()
    {
        if (playerHand.IsFull())
        {
            Debug.Log("Cannot pick up. Hand is full.");
            return;
        }

        playerHand.HoldItem(gameObject);
    }
}