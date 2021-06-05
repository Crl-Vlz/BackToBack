using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement_Behaviour : MonoBehaviour
{

    private SpriteRenderer sprite;

    public float aggroRange;

    public float speed;

    public Transform player;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    public void MoveMook()
    {

        float distance = Vector2.Distance(transform.position, player.position);

        gameObject.GetComponent<Enemy_attack_Behavior>().OnAttack(distance);

        if (distance <= aggroRange)
        {
            Vector3 movement = new Vector3(0f, 0f, 0f);

            if (player.position.x < transform.position.x)
            {
                movement = new Vector3(speed * -1, 0f, 0f) * Time.deltaTime;
                //transform.position += movement;
                sprite.flipX = true;
            }
            if (player.position.x > transform.position.x)
            {
                movement = new Vector3(speed, 0f, 0f) * Time.deltaTime;
                //transform.position += movement;
                sprite.flipX = false;
            }

            transform.position += movement;

        }

    }
}
