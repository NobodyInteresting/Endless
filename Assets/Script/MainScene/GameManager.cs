using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using System;

public class GameManager : MonoBehaviour {
    public GameObject EndScene;
    public GameObject Startmen;
    public GameObject scr;
    public GameObject PauseMen;
    private int EndofGameScore;
    private static bool restarted = false;
    private static bool ReturningafterPause = false;
    private static bool lostFocus = false;
    private static bool signedIn = false;
    private float[] volume;

    private void Awake()
    {
        //Google Play Services
        PlayGamesPlatform.Activate();
        OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);

    }

    public static void OnConnectClick()
    {
        Social.localUser.Authenticate((bool success) =>{
            OnConnectionResponse(success);
        });
    }

    public static void OnConnectionResponse(bool authenticated)
    {
        signedIn = authenticated;
    }

    public void LeaderBoards()
    {
        if (PlayerPrefs.GetInt("Snd") == 1)
            FindObjectOfType<AudioManager>().Play("ButtonClick");

        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void OnLeaderBoardsClick()
    {
        if (signedIn)
            LeaderBoards();
        else OnConnectClick();
          
    }
    public static void ReportScore(int score)
    {
        Social.ReportScore(score, EndlessGPS.leaderboard_high_score, (bool success) =>{});
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("World2"))
            PlayerPrefs.SetInt("WorldType", 2);
        else if (SceneManager.GetActiveScene().name.Equals("OnlyScene"))
            PlayerPrefs.SetInt("WorldType", 1);

        if (restarted)
        {
            restarted = false;
            StartGame();
        }        
    }

    public void EndGame()
    {
        //stop obstacles from spawning and find rigidbodies to destroy
        GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled = false;
        GameObject[] thingstoavoid = GameObject.FindGameObjectsWithTag("thingtoavoid");
        if (PlayerPrefs.GetInt("WorldType") == 2)
        {
            thingstoavoid = GameObject.FindGameObjectsWithTag("Respawn");          
        }

        for (int i = 0; i < thingstoavoid.Length; i++)
            {
                //freeze obstacles
                Destroy(thingstoavoid[i].GetComponent<Rigidbody>());
                if (thingstoavoid[i].GetComponent<ObstacleMovement>())
                    thingstoavoid[i].GetComponent<ObstacleMovement>().enabled = false; 
                //freeze player
                Destroy(GameObject.Find("Player").GetComponent<Rigidbody>());
                GameObject.Find("Player").GetComponent<Movement>().enabled = false;
            }

        if (thingstoavoid.Length == 0)
        {           
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
           //BLOW UP ANIMATION POSSIBLY
        }

        //get score at the end of game
        EndofGameScore = GameObject.Find("Detector").GetComponent<Score>().GetScore();

        //disable 
        scr.SetActive(false);
        EndScene.SetActive(true);
        ReportScore(PlayerPrefs.GetInt("HighScore"));

    }


    public void StartGame()
    {
        GameObject.Find("Player").GetComponent<Movement>().enabled = true;
        GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled = true;
        Startmen.SetActive(false);
        scr.SetActive(true);
    }


    public void RestartGame()
    {
        restarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int getEndgameScore()
    {
        return EndofGameScore;        
    }


   private void OnApplicationPause(bool pause)
    {
        if (pause && GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled) //if it's spawning, the game has started --> won't needlessly start the Pause Sequence Animation
        {
            ReturningafterPause = true;
            Pause();
        }        
        else if (pause && PauseMen.activeSelf)
        {
            PauseMen.SetActive(false);
            ReturningafterPause = true;
            PauseMen.GetComponent<Pause>().SetRestart(true);
        }
        else if (!pause && ReturningafterPause)
        {
            ReturningafterPause = false;
            PauseMen.SetActive(true);
            PauseMen.GetComponent<Pause>().SetRestart(true);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled)
        {
            lostFocus = true;
            Pause();
        }
        else if (!focus && PauseMen.activeSelf)
        {
            PauseMen.SetActive(false);
            PauseMen.GetComponent<Pause>().SetRestart(true);
            lostFocus = true;
        }
        else if (lostFocus && focus)
        {
            lostFocus = false;
            PauseMen.SetActive(true);
            PauseMen.GetComponent<Pause>().SetRestart(true);
        }
    }

    public void Pause()
    {
        GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled = false;//stop spawnining
        GameObject[] thingstoavoid = GameObject.FindGameObjectsWithTag("thingtoavoid");//array to store obstacles
        if (PlayerPrefs.GetInt("WorldType") == 2)
        {
            thingstoavoid = GameObject.FindGameObjectsWithTag("Respawn");
        }

        for (int i = 0; i < thingstoavoid.Length; i++)
        {
            //freeze obstacles
            Destroy(thingstoavoid[i].GetComponent<Rigidbody>());
            if(thingstoavoid[i].GetComponent<ObstacleMovement>())
                thingstoavoid[i].GetComponent<ObstacleMovement>().enabled = false;
            //freeze player
            Destroy(GameObject.Find("Player").GetComponent<Rigidbody>());
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
        } 
    }

    public void AfterPause()
    {
        GameObject.Find("Platform").GetComponent<SpawnObstacles>().enabled = true;//start spawnining
        GameObject[] thingstoavoid = GameObject.FindGameObjectsWithTag("thingtoavoid");//array to store obstacles
        if (PlayerPrefs.GetInt("WorldType") == 2)
        {
            thingstoavoid = GameObject.FindGameObjectsWithTag("Respawn");
        }

        for (int i = 0; i < thingstoavoid.Length; i++)
        {
            //move obstacles
            thingstoavoid[i].AddComponent<Rigidbody>();
            if(thingstoavoid[i].GetComponent<ObstacleMovement>())
                thingstoavoid[i].GetComponent<ObstacleMovement>().enabled = true;
            thingstoavoid[i].GetComponent<Rigidbody>().useGravity = false;
            //move player
            if (!(GameObject.Find("Player").GetComponent<Rigidbody>()))
            {
                GameObject.Find("Player").AddComponent<Rigidbody>();
            }            
            GameObject.Find("Player").GetComponent<Movement>().enabled = true;

            if (GameObject.Find("Player").GetComponent<BoxCollider>())
            {
                GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = true; //turn on gravity
                GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            }

            else
            {
                GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = false; //turn Off gravity
                GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
            }   
        }
    }

}
