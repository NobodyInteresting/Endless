using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndSceneBttn : MonoBehaviour {
    public Sprite MusicOn;
    public Sprite MusicOff;
    public Button music;
    public Button sound;
    public Sprite SoundOff;
    public Sprite SoundOn;
    const string url = "https://play.google.com/store/apps/details?id=com.ProtonGames.Endless";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("M") == 0)//if it's default set it to 1 because in my code 1 means on and 2 means off
            PlayerPrefs.SetInt("M", 1);

        if (PlayerPrefs.GetInt("Snd") == 0)
            PlayerPrefs.SetInt("Snd", 1);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("M") == 2)
        {
            FindObjectOfType<AudioManager>().Stop("Theme");
            music.GetComponent<Image>().sprite = MusicOff;
        }

        if (PlayerPrefs.GetInt("Snd") == 2)//if sound is off
        {
            sound.GetComponent<Image>().sprite = SoundOff;
        }
    }

    public void Restart()
     {
        if (PlayerPrefs.GetInt("Snd") == 1)
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        FindObjectOfType<GameManager>().RestartGame();
     }
    
    public void EnterShop()
    {
        if (PlayerPrefs.GetInt("Snd") == 1)
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Shop");
    }
    
    public void Rating()
    {
        if (PlayerPrefs.GetInt("Snd") == 1)
            FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.OpenURL(url);
    }
 

    /*
      * Snd - 1 - on
      * Snd - 2 - off
      * userClicked - 1 - false
      * userClicked - 2 - true 
      * PlayerPrefs
      */ 
    public void TurnOffMusic()
    {
        if (PlayerPrefs.GetInt("M") == 2)//if music is off
        {
            music.GetComponent<Image>().sprite = MusicOn;
            if (PlayerPrefs.GetInt("Snd") == 1)//if sound is disabled
                FindObjectOfType<AudioManager>().Play("Theme");
            PlayerPrefs.SetInt("M", 1);
            PlayerPrefs.SetInt("userClicked", 1);//user clicked for music to be on 
        }
        else if (PlayerPrefs.GetInt("M") == 1)//if music is on
        {
            music.GetComponent<Image>().sprite = MusicOff;
            PlayerPrefs.SetInt("M", 2);
            FindObjectOfType<AudioManager>().Stop("Theme");
            PlayerPrefs.SetInt("userClicked", 2);//user clicked for muisc to be off
        }

    }

    public void Sound()
    {
        if (sound.GetComponent<Image>().sprite == SoundOff)//if sound is off
        {
            sound.GetComponent<Image>().sprite = SoundOn;
            PlayerPrefs.SetInt("Snd", 1);
            if (PlayerPrefs.GetInt("userClicked") == 1)//if the user didn't click for music to be off
            {
                music.GetComponent<Image>().sprite = MusicOn;
                FindObjectOfType<AudioManager>().Play("Theme");
                PlayerPrefs.SetInt("M", 1);
            }

        }
        else if (sound.GetComponent<Image>().sprite == SoundOn)//if sound is on
        {
            sound.GetComponent<Image>().sprite = SoundOff;
            PlayerPrefs.SetInt("Snd", 2);

            if (PlayerPrefs.GetInt("M") == 2)//if music is set to be off
                PlayerPrefs.SetInt("userClicked", 2); //we remember that user wanted the music to be off
            PlayerPrefs.SetInt("M", 2);//if musics wasn't off, we now set it to off
            music.GetComponent<Image>().sprite = MusicOff;
            FindObjectOfType<AudioManager>().Stop("Theme");

        }

    }

}
