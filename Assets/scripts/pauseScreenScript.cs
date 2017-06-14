using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScreenScript : MonoBehaviour {
    
    public GameObject PUS;

    // Use this for initialization
    void Start () {
 
}
	
	// Update is called once per frame
	void Update () {

    }

    public void resume()
    {
        Debug.LogWarning("Hit");
        if (SceneManager.GetActiveScene().name == ("levelOne"))
        {
                PUS.SetActive(false);
                pauseScript.paused = false;
                Debug.LogWarning("unpaused");
                Time.timeScale = 1;
        }

     }
    

    public void quit()
    {
        Debug.LogWarning("Hit");
        if (SceneManager.GetActiveScene().name == ("levelOne"))
        {
            pauseScript.paused = false;
            SceneManager.LoadScene("TitleScene");
            Time.timeScale = 1;
        }
    }
}
