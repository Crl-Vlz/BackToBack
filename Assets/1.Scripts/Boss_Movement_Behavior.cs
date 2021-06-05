using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement_Behavior : MonoBehaviour
{

    public float aggroRange = 30;

    public float speed = 2;

    public Transform player;

    private Animator anim;

    private SpriteRenderer sprite;

    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rgBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves monster to player
    public void MoveBoss()
    {

        float distance = Vector2.Distance(transform.position, player.position);

        gameObject.GetComponent<Boss_Attack_Behavior>().OnAttack(distance);

        if (distance <= aggroRange)
        {
            Vector3 movement = new Vector3(0f, 0f, 0f);

            if (player.position.x < transform.position.x)
            {
                movement = new Vector3(speed * -1, 0f, 0f) * Time.deltaTime;
                //transform.position += movement;
                //rgBody.velocity = new Vector2(-speed, 0f);
                sprite.flipX = false;
                anim.SetBool("isWalking", true);
            }
            if (player.position.x > transform.position.x)
            {
                movement = new Vector3(speed, 0f, 0f) * Time.deltaTime;
                //transform.position += movement;
                //rgBody.velocity = new Vector2(speed, 0f);
                sprite.flipX = true;
                anim.SetBool("isWalking", true);
            }

            transform.position += movement;

        }
        else
        {
            anim.SetBool("isWalking", false);
        }

    }

}
