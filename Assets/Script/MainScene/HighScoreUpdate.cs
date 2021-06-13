using UnityEngine;
using UnityEngine.UI;

public class HighScoreUpdate : MonoBehaviour {
    public Text HighestScore;
    public Text endScore;
    private int highScore;

    void Start()
    {
        ///set "You Scored" and get highscore
        endScore.text = FindObjectOfType<GameManager>().getEndgameScore().ToString();
        highScore = PlayerPrefs.GetInt("HighScore");

        //change highscore if the score at the end of the game is higher
        if (FindObjectOfType<GameManager>().getEndgameScore() > highScore)
        {
            HighestScore.text = FindObjectOfType<GameManager>().getEndgameScore().ToString();
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<GameManager>().getEndgameScore());
        }

        else HighestScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
        
}
