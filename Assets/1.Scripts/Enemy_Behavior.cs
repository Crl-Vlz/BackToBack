using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Behavior : MonoBehaviour
{

    public enum TYPE
    {
        Mook,
        Boss
    };

    //Amount of hit points the enemy has
    public int hp;

    //HP after damage
    private int currentHP;

    public Animator anim;

    public SpriteRenderer sprite;

    public TYPE enemyClass;

    public float cooldown;

    public float timerForNextAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = hp;
        timerForNextAttack = 0;
    }

    private void Update()
    {
        //Only acts if the enemy is visible
        if (GetComponent<Renderer>().isVisible)
        {
            if (enemyClass == TYPE.Mook)
            {
                gameObject.GetComponent<Enemy_Movement_Behaviour>().MoveMook();
            }
            else if (enemyClass == TYPE.Boss)
            {
                gameObject.GetComponent<Boss_Movement_Behavior>().MoveBoss();
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP > 0)
        {
            anim.SetTrigger("isHurt");
        }
        else 
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject, gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);
            if(enemyClass == TYPE.Boss)
                SceneManager.LoadScene("Win");
        }


    }

}
