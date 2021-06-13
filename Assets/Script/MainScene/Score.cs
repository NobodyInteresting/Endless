using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreText;
    private int score = 0;
    private float timeSinceLastCollision;


	void OnTriggerEnter(Collider col)
    {
     
        if ((col.gameObject.tag == "thingtoavoid") && (Time.time - timeSinceLastCollision) >= FindObjectOfType<SpawnObstacles>().ReturnTimeBetweenWaveSpeed())
        {             
            timeSinceLastCollision = Time.time;
            score++;
            scoreText.text = score.ToString();
            if (PlayerPrefs.GetInt("Snd") == 1)
                FindObjectOfType<AudioManager>().Play("Point");
        }
	}

    public int GetScore()
    { 
        return score;
    }
}
