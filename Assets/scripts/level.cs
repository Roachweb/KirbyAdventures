using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    public int spawnLocation;
    //public vector3 spawnLocation;
    //public int spawnLocation;
    //public int spawnLocation;
    //public int spawnLocation;

    // Use this for initialization
    void Awake()
    {

        if (spawnLocation < 0)
            spawnLocation = 0;

        //call 'spawnplayer()' from game manager
        GameManager.instance.spawnPlayer(spawnLocation);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
