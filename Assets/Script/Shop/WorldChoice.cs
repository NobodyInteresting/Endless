using UnityEngine;

public class WorldChoice : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (Time.timeSinceLevelLoad > 0.5f && PlayerPrefs.GetInt("Snd") == 1)
            FindObjectOfType<AudioManager>().Play("WorldSelect");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "standardwrld")
        {
            PlayerPrefs.SetInt("CSS", 1);
            PlayerPrefs.SetInt("WorldType", 1);
        }
        else if (other.gameObject.name == "lowpolyworld")
        {
            PlayerPrefs.SetInt("CSS", 1);
            PlayerPrefs.SetInt("WorldType", 2);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerPrefs.SetInt("CSS", 2);
    }

}
