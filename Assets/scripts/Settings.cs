using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Image musicImage;
    public Image soundImage;
    public Image vibeImage;

    public Sprite onSprite;
    public Sprite offSprite;

    private bool musicEnabled = true;
    private bool soundEnabled = true;
    private bool vibeEnabled = true;

    UIcontroller uiscript;
    [SerializeField] GameObject uiScriptGm;

    private void Start()
    {
        uiscript = uiScriptGm.GetComponent<UIcontroller>();

        // Load saved sound settings or set default values
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        vibeEnabled = PlayerPrefs.GetInt("VibeEnabled", 1) == 1;

        // Set button sprites according to current sound settings
        UpdateButtonSprites();
    }
    private void Update()
    {
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        vibeEnabled = PlayerPrefs.GetInt("VibeEnabled", 1) == 1;

    }
    public void ToggleMusic()
    {
        
        if (musicEnabled)
        {

            uiscript.OffMusic();
        }
        if (!musicEnabled)
        {
            uiscript.OnMusic();
        }
        musicEnabled = !musicEnabled;
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        UpdateButtonSprites();
       
        // Add code here to enable/disable music
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        UpdateButtonSprites();
        // Add code here to enable/disable sound
    }

    public void ToggleVibe()
    {
        vibeEnabled = !vibeEnabled;
        PlayerPrefs.SetInt("VibeEnabled", vibeEnabled ? 1 : 0);
        UpdateButtonSprites();
        // Add code here to enable/disable vibration
    }

    public void UpdateButtonSprites()
    {
        soundImage.sprite = soundEnabled ? onSprite : offSprite;
        musicImage.sprite = musicEnabled ? onSprite : offSprite;
        vibeImage.sprite = vibeEnabled ? onSprite : offSprite;
    }
}
