using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;           //referencing the Rigidbody component on the Player object
    public float speed;                 //public means the values can be accessed and changed in Unity inspector
    public Text score;
    public Text winText;
    public Text livesText;
    public Text loseText;

    public AudioSource musicSource;     //tells unity which game object plays audio
    public AudioClip bgMusic;
    public AudioClip winMusic;

    Animator anim;

    private int scoreValue = 0;
    private int lives = 3;

    public int jumpForce = 3;

    private bool facingRight = true;
    private bool airBorne;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();         //searching for and saving the ref to Rigidbody component on Player obj
        anim = GetComponent<Animator>();            //same thing but looking for Animator
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        PlayMusic(bgMusic);
    }

    void Update(){

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetInteger("State", 1);        //run
        } else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
            anim.SetInteger("State", 0);        //idle
        } /*else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            anim.SetInteger("State", 2);        //jump
        }*/
        
    }

    // FixedUpdate is called when physics are applied or a physics event occurs
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");        //grabbing mvmt inputs from Unity (like WASD or arrow keys)
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));       //applying physics to Player object's rigidbody

        if (facingRight == false && hozMovement > 0){
            Flip();
        } else if (facingRight == true && hozMovement < 0){
            Flip();
        }

        if (verMovement > 0){
            anim.SetInteger("State", 2);    //jump if airborne
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){         //when objects collide, execute code
        if(collision.collider.tag == "Coin"){                       //if one of the objects is tagged with "Coin"
            scoreValue++;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
            if(scoreValue == 4){                            //level change
                lives = 3;
                livesText.text = "Lives: " + lives.ToString();
                transform.position = new Vector3(61.26f, 2.64f, 0.0f);
                
            } else if(scoreValue == 8){         //win condition
                winText.text = "You win! Game created by Jessica Gettys";
                PlayMusic(winMusic);
            }
        }

        if(collision.collider.tag == "Enemy"){                       //if one of the objects is tagged with "Enemy", do:
            if(scoreValue != 8){                    //prevents player from losing after winning
                lives--;
                livesText.text = "Lives: " + lives.ToString();
            }
            
            Destroy(collision.collider.gameObject);
            
            if(lives <= 0){                 //death
            loseText.text = "You lose";
            Destroy(gameObject);            //byebye, player
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision){      //when objects are in constant contact
        if(collision.collider.tag == "Ground"){
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
                rd2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);   //add sudden vertical impulse force of 3
            }
            anim.SetInteger("State", 0);        //stop jump anim
        }
    }

    private void PlayMusic(AudioClip audio){
        musicSource.clip = (audio);
        musicSource.Play();
    }

    void Flip(){
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
