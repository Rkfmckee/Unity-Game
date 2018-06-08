using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float speed = 5.0f;
    private float jumpHeight = 10.0f;
    private float gravityScale = 9.8f;
    private float decelerateAmount;
    private float xAxis;
    private PlayerInput input;
    private bool onGround = false;

    private void Start()
    {
        input = gameObject.GetComponent<PlayerInput>();
        //Default Movement Keys
        input.setLeftKey(KeyCode.A);
        input.setRightKey(KeyCode.D);
        input.setJumpKey(KeyCode.Space);

        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void FixedUpdate () 
    {
        getMovement();

    }

    private void getMovement() 
    {
        xAxis = input.getXAxis();
        if (!onGround) {
            decelerateAmount = .02f;
        } else {
            decelerateAmount = .1f;
        }

        float offsetX = (gameObject.GetComponent<BoxCollider2D>().size.x * .55f);
        float offsetY = (gameObject.GetComponent<BoxCollider2D>().size.y / 2);
        float length = .05f;
        RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(transform.position.x + (xAxis * offsetX), transform.position.y + offsetY), new Vector2(xAxis, 0), length);
        RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(transform.position.x + (xAxis * offsetX), transform.position.y - offsetY), new Vector2(xAxis, 0), length);

        if (input.getLeftKey() || input.getRightKey()) 
        {
            if (hitTop || hitBottom)
            {
                Debug.DrawRay(new Vector2(transform.position.x+(xAxis*offsetX), transform.position.y + offsetY), new Vector2(xAxis*length, 0), Color.white, 1f, false);
                Debug.DrawRay(new Vector2(transform.position.x+(xAxis*offsetX), transform.position.y - offsetY), new Vector2(xAxis*length, 0), Color.white, 1f, false);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else 
            {
                //Use PlayerInput to move the player.
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xAxis * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        } 
        else 
        {
            //Gradually slow down
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) > 0) 
            {
                if (!hitTop && !hitBottom)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * decelerateAmount, 0);
                }
                else 
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }

        if (input.getJumpKey() && onGround) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*speed;
            onGround = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Solid")
        {
            if (collision.contacts[0].normal.y > 0) 
            {
                onGround = true;
            }
        }
    }

    public bool isOnGround() 
    {
        return onGround;
    }
}
