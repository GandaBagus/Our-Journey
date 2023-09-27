using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    public float speed;
    public float jumpHeight;

    private float dirY, dirX;

    private bool isGrounded;

    public bool ClimbingAllowed { get; set; }
    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(dirX * speed, body.velocity.y);

        if (dirX > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }

        /*if (ClimbingAllowed)
        {
            dirY = Input.GetAxisRaw("Vertical") * speed;
        }*/
        
        anim.SetBool("run", dirX != 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("NOT GROUNDED");
        }
    }

    /*private void FixedUpdate()
    {
        
        if (ClimbingAllowed)
        {
            body.isKinematic = true;
            body.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            body.isKinematic = false;
            body.velocity = new Vector2(dirX, body.velocity.y);
        }
    }*/
}
