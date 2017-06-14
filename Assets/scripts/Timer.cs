using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerLabel;
    [SerializeField]
    [Tooltip("Enter time in seconds")]
    private float countDown;
    private float _time;
    Animator anim;

    void Update()
    {
       

        time = countDown - Time.timeSinceLevelLoad;

        if (time > 0)
        {
            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;

            //update the label value
            timerLabel.text = string.Format("Loading... "  + " {0:00} : {1:00} ", minutes, seconds, fraction);
        }
        if (time <= 0)
        {
            SceneManager.LoadScene("levelOne");
        }


    }

    public float time
    {
        get { return _time; }
        set { _time = value; }
    }
}