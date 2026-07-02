using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Image buttonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isMuted = false;

    public void ToggleMute()
    {
        isMuted = !isMuted;

        audioMixer.SetFloat("MasterVolume", isMuted ? -80f : 0f);

        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}