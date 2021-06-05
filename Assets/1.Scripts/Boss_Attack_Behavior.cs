using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_Behavior : MonoBehaviour
{

    [SerializeField] private LayerMask playerLayer;

    public Transform hitbox;

    public Animator anim;

    public GameObject player;

    private float hitboxL = 2f;

    private float hitboxW = 2f;

    //OnAttack is called to check ditance from player and attack
    public void OnAttack(float distance)
    {
        if (gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack > 0)
        {
            gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack -= Time.deltaTime;
        }
        else
        {
            int randValue = Random.Range(0, 1000);
            if (distance <= 3)
            {
                int damage = 0;

                //Checks how many collisions are there

                Vector2 size = new Vector2(hitboxL, hitboxW);

                Collider2D[] hits = Physics2D.OverlapBoxAll(hitbox.position, size, 0f, playerLayer);

                if (randValue < 100)
                {
                    anim.SetTrigger("attack3");
                    damage = 40;
                }

                else if (randValue < 400)
                {
                    anim.SetTrigger("attack2");
                    damage = 30;
                }

                else if (randValue < 1000)
                {
                    anim.SetTrigger("attack1");
                    damage = 20;
                }

                foreach (Collider2D playerHurtbox in hits)
                {
                    playerHurtbox.GetComponent<Player_Behavior>().TakeDamage(damage);
                    gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack = gameObject.GetComponent<Enemy_Behavior>().cooldown;
                    return;
                }

            }

        }
    }

}
