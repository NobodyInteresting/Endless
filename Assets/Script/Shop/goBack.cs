using UnityEngine;
using UnityEngine.UI;

public class goBack : MonoBehaviour {
    public Text world;

    private void Update()
    {
        if (PlayerPrefs.GetInt("CSS").Equals(2))
            world.text = " ";
        else if (PlayerPrefs.GetInt("WorldType").Equals(1))
            world.text = "CUBE WORLD";
        else if (PlayerPrefs.GetInt("WorldType").Equals(2))
            world.text = "POLY WORLD";         
    }

    public void Done()
    {
        PlayerPrefs.SetFloat("SPL", GameObject.Find("ShopPlatform").GetComponent<Hover>().getPlatformX());
        if (PlayerPrefs.GetInt("Snd") == 1)
        { FindObjectOfType<AudioManager>().Play("ButtonClick"); }
        FindObjectOfType<WorldManager>().LoadWorld();
    }
}
