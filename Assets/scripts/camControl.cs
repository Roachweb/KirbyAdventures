using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{

    //Public variable to store a reference to the player game object
    //Private variable to store the offset distance between the player and camera
    Camera myCamera;

    private GameObject camTarget;
    public float dampTime;
    private Vector3 velocity = Vector3.zero;
    

    // Use this for initialization
    void Start()
    {
        if (dampTime == 0)
        {
            dampTime = 0.15f;
            Debug.LogWarning("Damp time is set to: " + dampTime);
        }

        myCamera = GetComponent<Camera>();
        camTarget = GameObject.FindGameObjectWithTag("Player");

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - (playert.transform.position x,0,0);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (camTarget)
        {

            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            Vector3 point = myCamera.WorldToViewportPoint(camTarget.transform.position);

            Vector3 delta = camTarget.transform.position
                    - myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            destination.y = 0;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            /*
            Vector3 position = transform.position;
            position.x = camTarget.transform.position.x + 5f;
            transform.position = position;
            */
        }
    }
    
}

