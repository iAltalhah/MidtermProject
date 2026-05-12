using UnityEngine;

public class StonePuzzle : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerHand playerHand;

    [SerializeField] Transform stoneLocation1;
    [SerializeField] Transform stoneLocation2;
    [SerializeField] Transform stoneLocation3;

    [SerializeField] string prompt = "Press E to place stone";

    int stonesPlaced;

    public string InteractionPrompt => prompt;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        TryPlaceStone();
    }

    void TryPlaceStone()
    {
        GameObject currentItem = playerHand.GetCurrentItem();

        if (currentItem == null)
        {
            Debug.Log("You are not holding a stone.");
            return;
        }

        Transform correctLocation = GetCorrectStoneLocation(currentItem.name);

        if (correctLocation == null)
        {
            Debug.Log("This item does not belong in this puzzle: " + currentItem.name);
            return;
        }

        SnapStone(currentItem, correctLocation);

        playerHand.ClearCurrentItem();

        stonesPlaced++;

        if (stonesPlaced >= 3)
        {
            Debug.Log("Stone puzzle completed.");
        }
    }

    Transform GetCorrectStoneLocation(string stoneName)
    {
        switch (stoneName)
        {
            case "Stone1":
                return stoneLocation1;

            case "Stone2":
                return stoneLocation2;

            case "Stone3":
                return stoneLocation3;

            default:
                return null;
        }
    }

    void SnapStone(GameObject stone, Transform snapPoint)
    {
        stone.transform.SetParent(snapPoint);
        stone.transform.localPosition = Vector3.zero;
        stone.transform.localRotation = Quaternion.identity;

        Debug.Log("Placed stone: " + stone.name);
    }
}