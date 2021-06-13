using UnityEngine;

public class Hover : MonoBehaviour {
    private static float[] worldOffset;
    private static GameObject[] worlds;
    private float thisX;
    private float thixY;
    private float thisZ;

    private void Start()
    {
        worlds = GameObject.FindGameObjectsWithTag("Worldddd");
        worldOffset = new float[worlds.Length];
        thisX = transform.position.x;
        thixY = transform.position.y;
        thisZ = transform.position.z;

        for (int i = 0; i < worlds.Length; i++)
        {
           worldOffset[i] = (worlds[i].GetComponent<Transform>().position.x - transform.position.x);          //save the offset between platform and worlds
        }

        transform.position = new Vector3(PlayerPrefs.GetFloat("SPL"), thixY, thisZ);//start moving it to place where player left it at
       
    }

    private void Update()
    {
        if (thisX != transform.position.x) // if the platform has moved
        {
            for (int i = 0; i < worlds.Length; i++)
            {
               worlds[i].GetComponent<Transform>().position = new Vector3(transform.position.x+worldOffset[i], worlds[i].GetComponent<Transform>().position.y, worlds[i].GetComponent<Transform>().position.z); //set the world in the right place lmao

            }
        }       
    }

    void OnTriggerStay(Collider other) //rotate the worlds
    {    
        if (other.gameObject.tag == "Worldddd" && other.gameObject.name != "CSS")
        {
            other.GetComponent<Transform>().Rotate(new Vector3(0, 5, 0), 0.5f);
        }

    }

    public float getPlatformX()
    {
        return transform.position.x;
    }
}
