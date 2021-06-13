using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSc : MonoBehaviour {
    private int WorldType;
	
	void Start () {

        WorldType = PlayerPrefs.GetInt("WorldType");
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 2.5f)
        {
            
            if (WorldType == 1)
            {
                SceneManager.LoadScene("OnlyScene");
            }

            else SceneManager.LoadScene("World2");
        }
    }	
	
}
