using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{
    public Image soundImage;
    public Sprite soundOnSprite;
    public Sprite muteSprite;

    private bool isMuted = false;


    public void ToggleSound()
    {
        isMuted = !isMuted;

        soundImage.sprite = isMuted ? muteSprite : soundOnSprite;

        // Mute/unmute the audio
        AudioListener.volume = isMuted ? 0f : 1f;

        Debug.Log("Sound is now " + (isMuted ? "Muted" : "Unmuted"));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMuted = false;
        soundImage.sprite = soundOnSprite;
        AudioListener.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
