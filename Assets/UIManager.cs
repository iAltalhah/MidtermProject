using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlsPanel;

    // فتح الستنق
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    // إغلاق الستنق
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // فتح الكنترولرز فوق الستنق
    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    // إغلاق الكنترولرز فقط
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }
}