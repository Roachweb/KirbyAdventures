using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPower : MonoBehaviour {
    public float speed;
    public float lifeT;
   
   
    // Use this for initialization
    void Start()
    {


        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        if (lifeT <= 0)
        {
            lifeT = 3.0f;
        }

        if (speed <= 0)
        {
            speed = 7.0f;
        }

        //to destroy after a certain time based on lifeT
        Destroy(gameObject, lifeT);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "mainAbil")
        {
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
        }

    }
}