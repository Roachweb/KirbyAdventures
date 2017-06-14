using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class character : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField]
    private stat hitPoints;

    public float speed;

    public float jumpForce;

    public bool mainAbil;

    public bool isGrounded;

    public bool isPlatform;

    public LayerMask isGroundLayer;

    public LayerMask isPlatformLayer;

    public Transform groundCheck;

    public float groundCheckRadius;

    public Transform projectSpawnPoint;

    public starPower starPowerPrefab;
    public float starPowerForce;

    public throwPro throwProPrefab;
    public float throwProForce;

    Animator anim;

    public bool candbljump;
    public int jmpCnt;

    //flip Variable
    public bool isFacingRight;

   

    //Pause
    pauseScript Pus;

    //private void Awake()
   // {
       // hitPoints.Initialize();
   // } 

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody2D>();
       

        if (!rb)
        {
            Debug.LogWarning("No Rigidbody2D Found.");
        }
        if (speed <= 0)
        {
            speed = 5.0f;

            Debug.LogWarning("Default speeding to " + speed);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 5.5f;

            Debug.LogWarning("Default jumping force to " + jumpForce);
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.5f;

            Debug.LogWarning("Default GrChRas to " + groundCheckRadius);
        }

        if (!groundCheck)
        {
            Debug.LogWarning("No Ground Check found");
        }

        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.LogWarning("No Animator Found.");
        }

        if (!projectSpawnPoint)
        {
            Debug.LogWarning("No spawn point found");
        }

        if (!starPowerPrefab)
        {
            Debug.LogWarning("No prefab found");
        }

        if (starPowerForce == 0)
        {
            starPowerForce = 7.0f;
            Debug.LogWarning("Projectile force not set. Projectile force now set to " + starPowerForce);
        }

        if (!throwProPrefab)
        {
            Debug.LogWarning("No prefab found");
        }

        if (throwProForce == 0)
        {
            throwProForce = 7.0f;
            Debug.LogWarning("throwPro force not set. throPro force now set to " + throwProForce);
        }

        if (jmpCnt == 0)
        {
            jmpCnt = 4;
            Debug.LogWarning("Jump count now set to " + jmpCnt);
        }

        /*if (hitPoints == 0)
        {
            hitPoints = 100;
            Debug.LogWarning("Hit Points set to " + hitPoints);
           
        }*/

        hitPoints.Initialize();
    }

    // Update is called once per frame
    void Update() {

        if (pauseScript.paused == false)
        {

            float moveValue = Input.GetAxisRaw("Horizontal");

            // Calculate Movement
            rb.velocity = new Vector2(speed * moveValue, rb.velocity.y);

            //Animate when char moves
            anim.SetFloat("MovValue", Mathf.Abs(moveValue));

            if (isFacingRight && moveValue < 0)
            {
                flip();
            }
            else if (!isFacingRight && moveValue > 0)
            {
                flip();
            }

            //checks if on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position,
                groundCheckRadius, isGroundLayer);

            //checks if on platform
            isPlatform = Physics2D.OverlapCircle(groundCheck.position,
                groundCheckRadius, isPlatformLayer);
            /*
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jmpCnt > 0)
            {
                candbljump = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jmpCnt--;
            }
            else
            {
                if (candbljump = true && Input.GetKeyDown(KeyCode.Space))
                {

                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                    candbljump = false;
                }
            }
            */
            if (jmpCnt < 1)
            {
                candbljump = false;
            }
            else if (Input.GetButtonDown("Jump") && candbljump == true)
            {
                jmpCnt--;
                anim.SetBool("inAir", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            /*In Air animation*/
            if (isGrounded == true || isPlatform == true)
            {
                anim.SetBool("inAir", false);
                candbljump = true;
                jmpCnt = 4;
            }
            else if (isGrounded == false || isPlatform == false)
            {
                anim.SetBool("inAir", true);
                anim.SetFloat("MovValue", 0);
                /*jmpCnt--;
                if (jmpCnt <= 0)
                {
                    candbljump = false;
                }*/

            }


            //to ignore collision on key down to the to perameters that check collided
            /*
            I could store the players get componet tag into a variable or taget it
            directly

            if (Input.GetKeyDown(KeyCode.DownArrow) && isPlatform)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            */

            //Main ABILITY 
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                anim.SetBool("mainAbil", true);
                anim.SetBool("mainAbilHold", true);
                mainAbil = true;
                speed = 0;
                jumpForce = 0;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetBool("mainAbil", false);
                anim.SetBool("mainAbilHold", false);
                mainAbil = false;
                speed = 5f;
                jumpForce = 5.5f;
            }
            if ((Input.GetKeyDown(KeyCode.Z)) && (GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated))
            {
                anim.SetBool("attk", true);

                //calls for the fireStar function
                fireStar();
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                anim.SetBool("attk", false);

                //calls for the fireStar function
            }

            //main attak setUP******
            if ((Input.GetKeyDown(KeyCode.X)) && (!GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated))
            {
                anim.SetBool("mainAttk", true);

            }

            else if ((Input.GetKeyUp(KeyCode.X)) && (!GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated))
            {
                anim.SetBool("mainAttk", false);
                throwPro();
                //Fire main attk
            }

            //Duck Mechanic
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                anim.SetBool("duck", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                anim.SetBool("duck", false);
            }

            /*To activate Fat Kirby */

            if (GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated)
            {
                anim.SetBool("mainAbilIdle", true);
                speed = 3f;
                jumpForce = 0.3f;

            }

            if (!GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated)
            {
                anim.SetBool("mainAbilIdle", false);
                speed = 5f;
                jumpForce = 5.5f;

            }
           

           /* if (hitPoints.CurrentVal <= 0)
            {
                anim.SetBool("dead", true);
            }*/
        }
    }

    void fireStar()
    {
        // the instantiate function needs the the prefab, the position of the spawn point and the rotation.
        // WHAT AND WHERE
        starPower temp =
            Instantiate(starPowerPrefab, projectSpawnPoint.position, projectSpawnPoint.rotation);

        temp.speed = starPowerForce;


        if (!isFacingRight)
        {
            temp.speed *= -1;
        }

        GameObject.FindGameObjectWithTag("mainAbil").GetComponent<mainAbility>().activated = false;
    }
    void throwPro()
    {
        // the instantiate function needs the the prefab, the position of the spawn point and the rotation.
        // WHAT AND WHERE


        throwPro temp =
            Instantiate(throwProPrefab, projectSpawnPoint.position, projectSpawnPoint.rotation);

        temp.speed = throwProForce;


        if (!isFacingRight)
        {
            temp.speed *= -1;
        }


    }
    void flip()
    {
        /*isFacingRight = !isFacingRight;*/

        if (isFacingRight)

            isFacingRight = false;

        else

            isFacingRight = true;

        if (gameObject.tag != "MainCamera")
        {
            // whats is the scale
            Vector3 scaleFactor = transform.localScale;
            //copy that and instance and edit scale
            scaleFactor.x *= -1f;

            //apply that scale to it
            transform.localScale = scaleFactor;

        }
    }
    // optional collision for drag animation  
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "enemy") || (collision.gameObject.tag == "deadZone"))
        {
            anim.SetBool("hit", true);
            Damage(10f);
            //hitPoints.CurrentVal -= 10;
            //HealthBar.size = hitPoints / 100f;
        }
        else
        {
            anim.SetBool("hit", false);
            //HealthBar.size = hitPoints / 100f;
        }

        if (hitPoints.CurrentVal <= 0)
        {
            anim.SetBool("dead", true);
        }
    }

    public void Damage(float value)
    {
        hitPoints.CurrentVal -= value;
        Debug.LogWarning("Hit Points changed to " + hitPoints.CurrentVal);
    }

    public void dead()
    {
        SceneManager.LoadScene("GameOver");
    }

    /*public float hitPoints
    {
        get { return _hitPoints; }
        set { _hitPoints = value;

            if (_hitPoints > 100)
            {
                _hitPoints = 100;
            }

            Debug.LogWarning("Hit Points changed to " + _hitPoints);
           

        }
    }*/
}

