using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{

    public Animator anim;

    public SpriteRenderer sprite;

    private Rigidbody2D rgBody;

    private BoxCollider2D box;

    public float jumpSpeed =500f;

    [SerializeField] private LayerMask groundLayer;

    private bool IsGrounded()
    {
        float extra = 0.1f;
        RaycastHit2D rayHit = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + extra, groundLayer);
        Color rayColor;
        if(rayHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        return rayHit.collider != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        rgBody = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Flips sprite according to direction
        if(Input.GetAxis("Horizontal") < 0f)
        {
            sprite.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            sprite.flipX = false;
        }


        //Moves character
        if (Input.GetAxis("Horizontal") != 0f)
        {
            anim.SetBool("isDucking", false);
            anim.SetBool("isWalking", true);
        }
        else
            anim.SetBool("isWalking", false);

        //Attacks
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("isAttacking");
        }

        //Ducks
        if (Input.GetButtonDown("Down"))
            anim.SetBool("isDucking", true);

        //Checks when the player stops ducking ans starts animation
        if (Input.GetButtonUp("Down"))
        {
            anim.SetTrigger("isRising");
            anim.SetBool("isDucking", false);
        }

        //Grabs an object
        if (Input.GetButtonDown("Fire2") && !anim.GetBool("hasGrabbed"))
        {
            anim.SetTrigger("isGrabbing");
            anim.SetBool("hasGrabbed", true);
        }

        //Throws an object
        else if(Input.GetButtonDown("Fire2") && anim.GetBool("hasGrabbed"))
        {
            anim.SetTrigger("isThrowing");
            anim.SetBool("hasGrabbed", false);
        }

        bool grounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rgBody.AddForce(transform.up * jumpSpeed);
        }

        if (!grounded)
        {
            anim.SetBool("isJumping", true);
        }

        if (grounded)
        {
            anim.SetBool("isJumping", false);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime;
    }

}
