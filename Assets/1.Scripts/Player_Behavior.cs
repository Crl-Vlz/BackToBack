using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Behavior : MonoBehaviour
{

    public Animator anim;

    public SpriteRenderer sprite;

    private Rigidbody2D rgBody;

    private BoxCollider2D box;

    public float jumpSpeed =50f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private LayerMask throwableLayer;

    public Transform hitbox;

    public Transform throwbox;

    public int totalHP = 100;

    private int currentHP;

    private bool changedSide;

    //Checks if the player is currently on the ground
    private bool IsGrounded()
    {
        float extra = 0.3f;
        RaycastHit2D rayHit = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + extra, groundLayer);
        return rayHit.collider != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = totalHP;
        rgBody = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        changedSide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitbox.position.y <= -8)
            SceneManager.LoadScene("Lose game");

        //Checks the side of the character
        bool actualSide = changedSide;

        //Flips sprite according to direction
        if (Input.GetAxis("Horizontal") < 0f)
        {
            sprite.flipX = true;
            actualSide = true;
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            sprite.flipX = false;
            actualSide = false;
        }

        //Flips the hitbox
        if (changedSide != actualSide)
        {
            gameObject.transform.GetChild(1).localPosition *= -1;
            changedSide = actualSide;
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
            OnAttack(1f, 10);
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
            if (OnGrab())
            {
                anim.SetBool("hasGrabbed", true);
            }
        }

        //Throws an object
        else if(Input.GetButtonDown("Fire2") && anim.GetBool("hasGrabbed"))
        {
            OnThrow();
            anim.SetTrigger("isThrowing");
            anim.SetBool("hasGrabbed", false);
        }

        //Checks if the payer is jumping
        bool grounded = IsGrounded();

        //if the player is on the ground adds force to be able to jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rgBody.AddForce(Vector2.up * jumpSpeed);
        }


        //Changes animations if the player is jumping or not
        if (!grounded)
        {
            anim.SetBool("isJumping", true);
        }

        if (grounded)
        {
            anim.SetBool("isJumping", false);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal")*2, 0f, 0f);
        transform.position += movement * Time.deltaTime;
    }

    //Checks if colliders clash making an attack
    private void OnAttack(float hitboxSize, int damage)
    {
        //Checks how many collisions are there

        Vector2 size = new Vector2(3f, hitboxSize);

        Collider2D[] hits = Physics2D.OverlapBoxAll(hitbox.position, size, 0f, enemyLayer);



        foreach(Collider2D enemyHurtbox in hits)
        {
            enemyHurtbox.GetComponent<Enemy_Behavior>().TakeDamage(damage);
        }

    }

    private bool OnGrab()
    {

        //Checks if it grabbed something

        Vector2 size = new Vector2(2f, 0.5f);

        Collider2D[] grabbed = Physics2D.OverlapBoxAll(throwbox.position, size, 0f, throwableLayer);



        foreach (Collider2D truckGrabbox in grabbed)
        {
            truckGrabbox.GetComponent<Truck_Behavior>().Grabbed(gameObject);
            return true;
        }

        return false;

    }

    private void OnThrow()
    {
        float xForce = 1000f;
        float yForce = 10f;

        if (sprite.flipX)
        {
            xForce *= -1;
        }

        GameObject truck = gameObject.transform.GetChild(3).gameObject;

        truck.GetComponent<Truck_Behavior>().Thrown(new Vector2(xForce, yForce));

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(throwbox.position, new Vector3(2f, 0.5f, 1f));
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP > 0)
        {
            anim.SetTrigger("isHurt");
        }
        else 
        {
            SceneManager.LoadScene("Lose game");
        }

    }

}
