using UnityEngine;

public class Movement : MonoBehaviour {
    private float speed = 0.005f;
    private float thisX;
    private float startpos;
    private float changeinpos;

    private void Start()
    {
        thisX = transform.position.x;
    }


    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            startpos = touch.position.x;
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 pos = Input.GetTouch(0).deltaPosition;


            if (this.tag.Equals("ShopPlatform"))
            {
                if ((transform.position.x >= thisX && pos.x > 0) || (transform.position.x <= -4.7f && pos.x < 0)) // if the platform's position is less than or equal to stop point and the change in x is greater than zero stop
                {
                    return;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(pos.x * speed, 0, 0, ForceMode.Impulse);
                    transform.Translate(pos.x * speed, 0, 0);
                }
            }


            else
            {
                if (this.GetComponent<MeshCollider>())//spaceship
                    transform.Translate(pos.x * speed/2, pos.y * speed, 0);
                else transform.Translate(pos.x * speed/2, 0, 0); //regular cube player

                if (transform.position.y < -2 || transform.position.x < -2f || transform.position.x > 2.4f || transform.position.y > 4.75f)
                {
                    FindObjectOfType<GameManager>().EndGame();
                }
            }


        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && this.tag.Equals("ShopPlatform"))//if we're in the shop and the swipe/touch has ended
        {
            changeinpos = Mathf.Abs(startpos - Input.GetTouch(0).position.x);
            if (changeinpos <= 50f) //stop moving
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true; //disable rb
                gameObject.GetComponent<Rigidbody>().isKinematic = false; //enable rb


            }
        }
        else if (this.tag.Equals("ShopPlatform") && (transform.position.x >= thisX || transform.position.x <= -4.7f))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true; //disable rb
            gameObject.GetComponent<Rigidbody>().isKinematic = false; //enable rb
        }
        
    }
   
}
