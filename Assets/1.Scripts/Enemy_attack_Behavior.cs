using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_Behavior : MonoBehaviour
{

    [SerializeField] private LayerMask playerLayer;

    public Transform hitbox;

    public Animator anim;

    public GameObject player;

    public float hitboxL = 1f;

    public float hitboxW = 1f;

    public int damage;

    public void OnAttack(float distance)
    {
        if (gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack > 0)
        {
            gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack -= Time.deltaTime;
        }

        else if (distance <= 2)
        {
            //Checks how many collisions are there

            Vector2 size = new Vector2(hitboxL, hitboxW);

            Collider2D[] hits = Physics2D.OverlapBoxAll(hitbox.position, size, 0f, playerLayer);



            foreach (Collider2D playerHurtbox in hits)
            {
                playerHurtbox.GetComponent<Player_Behavior>().TakeDamage(damage);
                gameObject.GetComponent<Enemy_Behavior>().timerForNextAttack = gameObject.GetComponent<Enemy_Behavior>().cooldown;
                return;
            }

            anim.SetTrigger("isAttacking");
        }

    }

}
