using UnityEngine;

public class ObstacleMovement : MonoBehaviour {
    public Rigidbody rb;
    private float whenToDisappear = -7.834276f;
    private int speed = 12;
    private float timeToChangeSpeed;

    private void Start()
    {
        timeToChangeSpeed = 7f;

        if (rb.gameObject.name.Equals("Obstacle(Clone)"))
            whenToDisappear = -12f;
    }

    void Update()
    {        
        if (transform.position.z < whenToDisappear)
            Destroy(gameObject);

        if ((GameObject.Find("Platform").GetComponent<SpawnObstacles>().CurrentTime() >= timeToChangeSpeed && speed <= 20 ))
        {
            speed = speed + 5;
            timeToChangeSpeed = timeToChangeSpeed + 10; 
            transform.Translate(Vector3.back * Time.deltaTime * (speed + 5), Space.World);
        }
        else
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }        

    }

   
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        { 
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    

}
