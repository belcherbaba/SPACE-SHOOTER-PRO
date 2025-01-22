using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update

    //id fow power ups
    //0==triple shot
    //1== speed
    //2 == sheild
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private float _speed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        //when we leave the screen , destroy object

        transform.Translate(Vector3.down*_speed*Time.deltaTime);

        if (transform.position.y<-7f)
        {
            Destroy(this.gameObject);
        }


    }
     private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player1")
        {

            Player1 player = other.transform.GetComponent<Player1>();
            if (player != null)
            {
                

                switch (powerupID)
                {
                    case 0:
                     player.TripleShotActive();
                     break;
                     
                    case 1:
                      player.SpeedBoostActive();
                      break;

                    case 2:
                       player.ShieldActive();
                       break;

                    default:
                    Debug.Log("Default");
                    break;   

                }
               
            }
            Destroy(this.gameObject);
        }
    }
}
