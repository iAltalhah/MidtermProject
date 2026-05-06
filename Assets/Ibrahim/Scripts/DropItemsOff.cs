using UnityEngine;

public class DropItemsOff : MonoBehaviour
{
    [SerializeField] Transform gemOnePosition;
    [SerializeField] Transform gemTwoPosition;
    [SerializeField] Transform gemThreePosition;
    [SerializeField] Transform gemFourPosition;
    [SerializeField] Transform gemFivePosition;

    public bool CheckTheItem(GameObject gem)
    {
        if (gem == null)
        {
            Debug.Log("No item in hand.");
            return false;
        }

        switch (gem.name)
        {
            case "Gem1":
                SnapItem(gem, gemOnePosition);
                return true;

            case "Gem2":
                SnapItem(gem, gemTwoPosition);
                return true;

            case "Gem3":
                SnapItem(gem, gemThreePosition);
                return true;

            case "Gem4":
                SnapItem(gem, gemFourPosition);
                return true;

            case "Gem5":
                SnapItem(gem, gemFivePosition);
                return true;

            default:
                Debug.Log("This item does not belong here: " + gem.name);
                return false;
        }
    }

    void SnapItem(GameObject gem, Transform snapPoint)
    {
        gem.transform.SetParent(snapPoint);
        gem.transform.localPosition = Vector3.zero;
        gem.transform.localRotation = Quaternion.identity;

        Debug.Log("Placed item: " + gem.name);
    }
}