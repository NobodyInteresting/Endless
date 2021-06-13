using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {    

    public void LoadWorld()
    {
       
        int type = PlayerPrefs.GetInt("WorldType");

        if (type == 1)
            SceneManager.LoadScene("OnlyScene");
        else if (type == 2)
            SceneManager.LoadScene("World2");
        else SceneManager.LoadScene("OnlyScene");
    }
}

