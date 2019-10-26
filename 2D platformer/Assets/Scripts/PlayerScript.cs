using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text countText;

    public Text winText;

    public Text livesText;

    public Text loseText;

    public AudioSource WinMusic;

    public Animator animator;

    private int count;
    private int lives;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        loseText.text = "";
        lives = 3;
        SetCountText();
        SetLivesText();
        WinMusic = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        animator.SetFloat("Speed", Mathf.Abs(hozMovement));

      
   
      

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetInteger("State", 2);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject);

            if (count == 4)
            {
                transform.position = new Vector2(78.0f, 3.0f);
                lives = 3;
                SetLivesText();
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            WinMusic.Play();
            winText.text = "You Win! Game created by Paul Frappollo";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            animator.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetInteger("State", 1);
                rd2d.AddForce(new Vector2(0, 9), ForceMode2D.Impulse);
            }
        }
  

    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            Destroy(this);
            loseText.text = "You Lose! Game created by Paul Frappollo";
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}