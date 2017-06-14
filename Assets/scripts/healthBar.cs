using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {


    /// <summary>
    /// Need to fix health bar
    /// use https://www.youtube.com/watch?v=ox-QiHfUqdI
    /// for reference
    /// </summary>
   
    //public Scrollbar HealthBar;
    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
    // Use this for initialization
    void Start () {

      

        //Value =
	}
	
	// Update is called once per frame
	void Update () {


//if (cc.hitPoints >= 0)
        //{


            handleBar();
       // }
       /* if (HealthBar.size >= 0)
        {
            HealthBar.size = cc.hitPoints / 100f;
        }*/
    }

    private void handleBar()
    {
       


        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
       
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
