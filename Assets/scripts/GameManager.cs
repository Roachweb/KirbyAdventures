using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public GameObject playerPrephab;

    int _score;

    // Use this for initialization
    void Start()
    {

        if (instance)
            DestroyImmediate(gameObject);
        else
        {// new gameManager can be created to carry over fresh variable and progress levels
            instance = this;

            DontDestroyOnLoad(this);
        }

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
                SceneManager.LoadScene("Instructions");
            else if (SceneManager.GetActiveScene().name == "Instructions")
                SceneManager.LoadScene("levelOne");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Two scenes "Pause" useful
            // usine index i can add one to the index to get to next level

            if (SceneManager.GetActiveScene().name == "GameOver")
                SceneManager.LoadScene("TitleScene");

        }
    }

    //Called when the 'character' is spawned
    public void spawnPlayer(int spawnLocation)
    //public void spawnPlayer(Vector3 spawnLocation)
    //public void spawnPlayer(Transform spawnLocation)
    //public void spawnPlayer(GameObject spawnLocation)
    {
        //requires spawn point to be named (SceneNumber)_(number)
        // levelOne_0
        string spawnPointName = SceneManager.GetActiveScene().name
            + "_" + spawnLocation;

        //Find location to spawn 'character' at
        Transform spawnPointtransform = GameObject.Find(spawnPointName).GetComponent<Transform>();

        //Instantiate (create) 'Character' spawn point
        Instantiate(playerPrephab, spawnPointtransform.position,
            spawnPointtransform.rotation);



    }

    public void playLevel()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void startOver()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void quit()
    {

        if (SceneManager.GetActiveScene().name == "levelOne")
            SceneManager.LoadScene("TitleScene");
        else if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }
    }

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int score
    {
        get { return _score; }
        set { _score = value; }
    }
}
