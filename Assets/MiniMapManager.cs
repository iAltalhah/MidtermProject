using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public RectTransform miniMap;

    void Start()
    {
        int pos = PlayerPrefs.GetInt("MiniMapPosition", 0);

        Debug.Log("MiniMap Position = " + pos);

        // Top Left
        if (pos == 0)
        {
            miniMap.anchorMin = new Vector2(0, 1);
            miniMap.anchorMax = new Vector2(0, 1);
            miniMap.pivot = new Vector2(0, 1);
            miniMap.anchoredPosition = new Vector2(20, -20);
        }

        // Top Right
        if (pos == 1)
        {
            miniMap.anchorMin = new Vector2(1, 1);
            miniMap.anchorMax = new Vector2(1, 1);
            miniMap.pivot = new Vector2(1, 1);
            miniMap.anchoredPosition = new Vector2(-20, -20);
        }

        // Bottom Left
        if (pos == 2)
        {
            miniMap.anchorMin = new Vector2(0, 0);
            miniMap.anchorMax = new Vector2(0, 0);
            miniMap.pivot = new Vector2(0, 0);
            miniMap.anchoredPosition = new Vector2(20, 20);
        }

        // Bottom Right
        if (pos == 3)
        {
            miniMap.anchorMin = new Vector2(1, 0);
            miniMap.anchorMax = new Vector2(1, 0);
            miniMap.pivot = new Vector2(1, 0);
            miniMap.anchoredPosition = new Vector2(-20, 20);
        }
    }
}