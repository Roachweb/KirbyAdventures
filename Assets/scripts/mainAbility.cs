using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class mainAbility : MonoBehaviour {

    Animator anim;

    public bool mAB;

    public GameObject starObj;

    public bool activated;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mAB = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            mAB = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "starCol") && (mAB == true))
        {
            activated = true;
            Debug.Log("taged star");
            Destroy(other.gameObject, 0.5f);
        }
    }
   


}

