using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_Behavior : MonoBehaviour
{

    [SerializeField] private LayerMask playerLayer;

    public Transform hitbox;

    private Animator anim;

    public GameObject player;

    public float hitboxL = 1f;

    public float hitboxW = 1f;

    public float damage;

    public int attackRange;

    private enum TYPE
    {
        Mook,
        Boss
    };

    [SerializeField] private TYPE enemyClass;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAttack(float distance)
    {

        if (gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack > 0)
        {
            gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack -= Time.deltaTime;
        }

        else if (distance <= attackRange)
        {
            //Checks how many collisions are there

            Vector2 size = new Vector2(hitboxL, hitboxW);

            Collider2D[] hits = Physics2D.OverlapBoxAll(hitbox.position, size, 0f, playerLayer);

            float damageMade = damage;;

            if (enemyClass == TYPE.Mook)
            {
                anim.SetTrigger("isAttacking");

            }

            else if(enemyClass == TYPE.Boss)
            {

                //Gets a random value betwen 0-999
                int randValue = Random.Range(0, 1000);

                if (randValue < 100 && randValue >= 0)
                {
                    anim.SetTrigger("attack3");
                    damageMade *= 2;
                }

                else if (randValue < 400 && randValue >= 100)
                {
                    anim.SetTrigger("attack2");
                    damageMade *= 1.5f;
                }

                else if (randValue < 1000 && randValue >= 400)
                {
                    anim.SetTrigger("attack1");
                }

            }

            foreach (Collider2D playerHurtbox in hits)
            {
                playerHurtbox.GetComponent<Player_Behavior>().TakeDamage((int)damageMade);
                gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack = gameObject.GetComponent<Enemy_Behavior>().cooldown;
                return;
            }

        }

    }

}
