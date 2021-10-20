using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;           //referencing the Rigidbody component on the Player object
    public float speed;                 //public means the values can be accessed and changed in Unity inspector
    public Text score;
    private int scoreValue = 0;
    public int jumpForce = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();         //searching for and saving the ref to Rigidbody component on Player obj
        score.text = scoreValue.ToString();
    }

    void Update(){
        if(Input.GetKey("escape")){
            Application.Quit();
        }
    }

    // FixedUpdate is called when physics are applied or a physics event occurs
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");        //grabbing mvmt inputs from Unity (like WASD or arrow keys)
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));       //applying physics to Player object's rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision){         //when objects collide, execute code
        if(collision.collider.tag == "Coin"){                       //if one of the objects is tagged with "Coin"
            scoreValue++;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision){      //when objects are in constant contact
        if(collision.collider.tag == "Ground"){
            if(Input.GetKey(KeyCode.W)){
                rd2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);   //add sudden vertical impulse force of 3
            }
        }
    }

}
