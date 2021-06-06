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
        if (GetComponent<Renderer>().isVisible && !CheckIfDead())
        {
            gameObject.GetComponent<Enemy_Movement_Behaviour>().MoveEnemy();
        }

        if (CheckIfDead())
        {
            if (enemyClass == TYPE.Boss)
            {
                StartCoroutine(Wait(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 3.0f, "Win"));
            }
            Destroy(gameObject, gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 4.0f);
        }

    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        anim.SetTrigger("isHurt");
    }

    private bool CheckIfDead()
    {
        if (currentHP > 0)
        {
            return false;
        }
        else
        {
            anim.SetBool("isDead", true);
            return true;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator Wait(float seconds, string scene)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scene);
    }

}
