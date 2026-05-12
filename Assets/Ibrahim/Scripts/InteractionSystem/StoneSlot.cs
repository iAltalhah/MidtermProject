using UnityEngine;

public class StoneSlot : MonoBehaviour
{
    [SerializeField] PlayerHand playerHand;


    [SerializeField] string acceptedStoneName;
    [SerializeField] Transform snapPoint;

    [SerializeField] GameManager gameManager;


    bool isFilled;

    public void PlaceStone()
    {
        if (isFilled)
        {
            Debug.Log("This stone slot is already filled.");
            return;
        }

        GameObject heldItem = playerHand.GetCurrentItem();

        if (heldItem == null)
        {
            Debug.Log("You are not holding a stone.");
            return;
        }

        if (heldItem.name != acceptedStoneName)
        {
            Debug.Log("Wrong stone. This slot accepts: " + acceptedStoneName);

            ItemPickUp itemPickUp = heldItem.GetComponent<ItemPickUp>();

            if (itemPickUp != null)
            {
                itemPickUp.ReturnToOriginalPosition();
            }

            playerHand.ClearCurrentItem();
            return;
        }

        SnapStone(heldItem);
        playerHand.ClearCurrentItem();

        isFilled = true;

        gameManager.stoneCount++;

        Debug.Log("Correct stone placed: " + heldItem.name);
    }

    void SnapStone(GameObject stone)
    {
        stone.transform.SetParent(snapPoint);
        stone.transform.localPosition = Vector3.zero;
        stone.transform.localRotation = Quaternion.identity;

        Collider stoneCollider = stone.GetComponent<Collider>();

        if (stoneCollider != null)
            stoneCollider.enabled = false;
    }
}