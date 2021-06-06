using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement_Behaviour : MonoBehaviour
{

    private SpriteRenderer sprite;

    public float aggroRange;

    public float speed;

    public Transform player;

    [SerializeField] bool isFlipped = false;

    private bool changedSide;

    private enum MOVEMENT
    {
        Ground,
        Air
    };

    [SerializeField] private MOVEMENT move;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        changedSide = false;
    }

    // Update is called once per frame
    public void MoveEnemy()
    {

        //Distance from the player
        float distance = Vector2.Distance(transform.position, player.position);

        //Distance moved from x
        float moveX = 0f;

        //Distance moved from Y
        float moveY = 0f;

        gameObject.GetComponent<Enemy_attack_Behavior>().OnAttack(distance);

        if (distance <= aggroRange)
        {
            Vector3 movement;

            gameObject.GetComponent<Animator>().SetBool("isWalking", true);

            bool actualSide = changedSide;

            if (player.position.x < transform.position.x)
            {
                actualSide = false;
                moveX = -speed * Time.deltaTime;
                if (!isFlipped)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;
            }
            if (player.position.x > transform.position.x)
            {
                actualSide = true;
                moveX = speed * Time.deltaTime;
                if (!isFlipped)
                    sprite.flipX = true;
                else
                    sprite.flipX = false;
            }

            else
            {
                gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            }

            //Flips the hitbox
            if (changedSide != actualSide)
            {
                gameObject.transform.GetChild(0).localPosition *= -1;
                changedSide = actualSide;
            }

            //If the enemy is aerial move from y also
            if (move == MOVEMENT.Air)
            {
                if (player.position.y < transform.position.y)
                {
                    moveY = -speed / 2 * Time.deltaTime;
                }
                if (player.position.y > transform.position.y)
                {
                    moveY = speed / 2 * Time.deltaTime;
                }
            }

            movement = new Vector3(moveX, moveY, 0f);

            transform.position += movement;

        }

    }
}
