using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{

    GameObject _PS;

    public static bool paused;

    // Use this for initialization
    void Start()
    {

        PS = GameObject.Find("Canvas_Pause");

        PS.SetActive(false);

        paused = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if ((PS.activeInHierarchy) && (SceneManager.GetActiveScene().name == ("levelOne")))
            {
                PS.SetActive(false);
                paused = false;
                Debug.LogWarning("unpaused");
                Time.timeScale = 1;
            }

            else if ((!PS.activeInHierarchy) && (SceneManager.GetActiveScene().name == ("levelOne")))
            {
                PS.SetActive(true);
                Time.timeScale = 0;
                Debug.LogWarning("paused");
                paused = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (SceneManager.GetActiveScene().name == ("levelOne"))
            {
                Debug.Log("Quit Game");
            }


        }
    }

    public GameObject PS
    {
        get { return _PS; }
        set { _PS = value; }
    }
}