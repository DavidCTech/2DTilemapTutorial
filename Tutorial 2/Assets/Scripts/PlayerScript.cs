using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
    public Text score;
    public Text lives;
    public GameObject WinText;
    public GameObject LossText;
    private int scoreValue;
    private int scoreTwo;
    private int livesValue;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        livesValue = 3;
        scoreValue = 0;
        scoreTwo = 0;
        SetCountText();

        musicSource.clip = musicClipTwo;
        musicSource.Play();
        WinText.SetActive(false);
        LossText.SetActive(false);
    }

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.W))

        {
          anim.SetInteger("State", 2);
         }

     if (Input.GetKeyUp(KeyCode.W))

        {
          anim.SetInteger("State", 0);
         }

     if (Input.GetKeyDown(KeyCode.D))

        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.D))

        {
          anim.SetInteger("State", 0);
         }
          if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.A))

        {
          anim.SetInteger("State", 0);
         }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            scoreTwo += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
       if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
       if (scoreTwo == 4)
       {
           scoreTwo += 1;
           transform.position = new Vector2(44.0f, 1.0f);
           livesValue = 3;
           SetCountText();
       }
    }
   

    void SetCountText()
    {
        score.text = "Score: " + scoreValue.ToString();
       if (scoreValue >= 8)
        {
            WinText.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
            speed = 0;
            jumpForce = 0;
        }  
        lives.text = "Lives: " + livesValue.ToString();
       if (livesValue <= 0)
        {
            LossText.SetActive(true);
            speed = 0;
            jumpForce = 0;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}