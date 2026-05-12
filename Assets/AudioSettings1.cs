using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}