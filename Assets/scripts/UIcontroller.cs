using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject MenuUI, SettingsUI, ShopUI, LevelChoiseUI, HtpUI, PauseUi, GameOverUI, GameUI,Level1, Level2, Level3,Fon,Player1, Player2, Player3;
    [SerializeField] Animator Cutscene;
    [SerializeField] Image HpImage;
    [SerializeField] Text moneyTextShop, moneyTextGame, moneyTextGameOver;
    [SerializeField] Sprite[] PlayerSkins;
    [SerializeField] AudioClip Click, GameMusic, GameOver, MenuMusic,NoMoneySound;
    float hp=1;
    bool inGame=false;
    public string[] tagsToDeactivate;
    int YouNowLevel = 0;
    AudioSource audio;
    Settings settings;
    [SerializeField] GameObject settingsGM;
    [SerializeField] Image UltaImage;
    [SerializeField] Image VolImage;
    [SerializeField] Sprite Off, On;
    Player playerScript;
    void Start()
    {
        settings = settingsGM.GetComponent<Settings>();
        audio = GetComponent<AudioSource>();
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("money", 100000);
        PlayerPrefs.SetInt("SoundEnabled", 1);
        PlayerPrefs.SetInt("VibeEnabled", 1);
        PlayerPrefs.SetInt("MusicEnabled", 1);
        playerScript = Player1.GetComponent<Player>();
        playerScript = Player3.GetComponent<Player>();
        playerScript = Player2.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
      
       HpImage.fillAmount = hp;
        moneyTextShop.text = PlayerPrefs.GetInt("money", 0).ToString();
        moneyTextGame.text = PlayerPrefs.GetInt("money", 0).ToString();
        moneyTextGameOver.text = PlayerPrefs.GetInt("money", 0).ToString();
      
        UltaImage.fillAmount = PlayerPrefs.GetFloat("ulta");
    }
    public void PlayLevel(int levelNum)
    {        
        // Set player sprite based on the selected character
        switch (PlayerPrefs.GetInt("ActiveCharacter", 0))
        {
            case 0:
                SetPlayerSkins(PlayerSkins[0]);
                break;
            case 1:
                SetPlayerSkins(PlayerSkins[1]);
                break;
            case 2:
                SetPlayerSkins(PlayerSkins[2]);
                break;
            case 3:
                SetPlayerSkins(PlayerSkins[3]);
                break;
            default:
                SetPlayerSkins(PlayerSkins[0]);
                break;
        }

        if (levelNum == 1)
        {
            Cutscene.Play("sleep");
            PlayerPrefs.SetInt("level", 1); ;
            StartLevel(GameMusic, Level1);
        }
        else if (levelNum == 2)
        {
            
            if (PlayerPrefs.GetInt("Background_1") == 1)
            {
                Cutscene.Play("sleep");
                StartLevel(GameMusic, Level2);
                PlayerPrefs.SetInt("level", 2); ;
            }
            else
            {
                PlayNoMoneySound();
            }
        }
        else if (levelNum == 3)
        {
            
            if (PlayerPrefs.GetInt("Background_2") == 1)
            {
                Cutscene.Play("sleep");
                StartLevel(GameMusic, Level3);
                PlayerPrefs.SetInt("level", 3); ;
            }
            else
            {
                PlayNoMoneySound();
            }
        }

        YouNowLevel = levelNum;
        PlayerPrefs.SetInt("koff", 1);

        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            Handheld.Vibrate();
        }
    }

    private void SetPlayerSkins(Sprite playerSkin)
    {
        Player1.GetComponent<SpriteRenderer>().sprite = playerSkin;
        Player2.GetComponent<SpriteRenderer>().sprite = playerSkin;
        Player3.GetComponent<SpriteRenderer>().sprite = playerSkin;
    }

    private void StartLevel(AudioClip music, GameObject level)
    {
        ClickSound();
        PlayMusic(music);
        Time.timeScale = 1;
        Fon.SetActive(false);
        inGame = true;
        Ochist();
        GameUI.SetActive(true);
        level.SetActive(true);

        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            HtpUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void PlayMusic(AudioClip music)
    {
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            audio.clip = music;
        }
        else
        {
            audio.clip = null;
        }
        audio.Play();
    }

    private void PlayNoMoneySound()
    {
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(NoMoneySound);
        }
    }

    public void Ochist()
    {
        MenuUI.SetActive(false);
        SettingsUI.SetActive(false);
        ShopUI.SetActive(false);
        LevelChoiseUI.SetActive(false);
        HtpUI.SetActive(false);
        PauseUi.SetActive(false);
        GameOverUI.SetActive(false);
        LevelChoiseUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameUI.SetActive(false);
        Level1.SetActive(false);
        Level2.SetActive(false);
        Level3.SetActive(false);
    }
    public void ToMenuUI()
    {
        
        // Проходим по каждому тегу в массиве
        foreach (string tagToRemove in tagsToDeactivate)
        {
            // Находим все объекты на сцене с заданным тегом
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToRemove);

            // Проходим по каждому найденному объекту и удаляем его
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
        Fon.SetActive(true);
        Time.timeScale = 1;
        hp = 1;
        Ochist();
        MenuUI.SetActive(true);
        ClickSound();
       
    }
    public void ToMenuUIPause()
    {
        Cutscene.Play("sleep");
        // Проходим по каждому тегу в массиве
        foreach (string tagToRemove in tagsToDeactivate)
        {
            // Находим все объекты на сцене с заданным тегом
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToRemove);

            // Проходим по каждому найденному объекту и удаляем его
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
        Fon.SetActive(true);
        Time.timeScale = 1;
        hp = 1;
        Ochist();
        MenuUI.SetActive(true);
        ClickSound();
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            audio.clip = MenuMusic;
            audio.Play();
        }
        else if (inGame)
        {
            audio.clip = null;
            audio.Play();
        }
    }
    public void Retry()
    {
        ToMenuUI();
        PlayLevel(YouNowLevel);
        ClickSound();
    }
    public void ToSettingsUI()
    {
        Ochist();
        SettingsUI.SetActive(true);
        settings.UpdateButtonSprites();
        ClickSound();
    }
    public void ToShopUI()
    {
        Ochist();
        ShopUI.SetActive(true);
        ClickSound();
    }
    public void ToLevelChoiseUI()
    {
        Ochist();
        LevelChoiseUI.SetActive(true);
        ClickSound();
    }
    public void ToHtpUI()
    {
        Ochist();
        HtpUI.SetActive(true);
    }
    public void ToPauseUI()
    {
       
        PauseUi.SetActive(true);
        Time.timeScale = 0;
        ClickSound();
        if (PlayerPrefs.GetInt("SoundEnabled") == 0)
        {
            VolImage.GetComponent<Image>().sprite = Off;
           
        }
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            VolImage.GetComponent<Image>().sprite = On;
            
        }
    }
    public void OutHTP()
    {
       
            PlayerPrefs.SetInt("first", 1);
            HtpUI.SetActive(false);
            Time.timeScale = 1;
        
    }
    public void ToPauseContinueUI()
    {
       
        PauseUi.SetActive(false);
        Time.timeScale = 1;
        ClickSound();
        
    }
    public void MinusHP(float damage)
    {
        if (hp<=0.1f)
        {
            Dead();
        }
        if (inGame)
        {
            hp -= damage/100;
        }
    }
    public void Dead()
    {
        
            PlayerPrefs.SetInt("koff", 1);
            
        
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            audio.clip = MenuMusic;
            audio.Play();
        }
        else
        {
            audio.clip = null;
            audio.Play();
        }
        Time.timeScale = 0;
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(GameOver);
        }
        GameOverUI.SetActive(true);
        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            Handheld.Vibrate();
        }
    }
    public void ClickSound()
    {
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(Click);
        }
    }
    public void OnMusic()
    {
        audio.clip = MenuMusic ; audio.Play();
    }
    public void OffMusic()
    {
        audio.clip = null; audio.Play();
    }
    public void SoundSwitch()
    {
        
        if (PlayerPrefs.GetInt("SoundEnabled")==1) 
        {
           
            VolImage.GetComponent<Image>().sprite = Off;
            PlayerPrefs.SetInt("SoundEnabled", 0);
            OffMusic();
            PlayerPrefs.SetInt("MusicEnabled", 0);
        }
        else
        {
           
            VolImage.GetComponent<Image>().sprite = On;
            PlayerPrefs.SetInt("SoundEnabled", 1);
            audio.clip = GameMusic; audio.Play();
        }
    }
}

