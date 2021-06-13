using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
    public GameObject pausemen;
    public Text countdown;
    private float time;
    private int timeToChange;
    private int countdw;
    private bool restart = false;

    private void Start()
    {
        countdw = 4;
        timeToChange = 1; 
    }

    // Update is called once per frame
    void Update () {
        time = time + Time.deltaTime;

        if (restart)
        {
            countdw = 4;
            countdown.text = "II";
        }
        if (time >= timeToChange && time < (timeToChange + 1))//change per second
        {
            if(countdw == 4)
                countdown.text = "II";
            else countdown.text = countdw.ToString();
            timeToChange++;
            countdw--;
            restart = false;

            if (countdw == -1)
                {
                   FindObjectOfType<GameManager>().AfterPause();
                   pausemen.SetActive(false);
                }         
        }
		
	}

    public void SetRestart(bool r)
    {
        restart = r;
    }
}
