using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_On_Collision : MonoBehaviour
{

    private bool thrown;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        thrown = false;
    }

    public void WasThrown()
    {
        thrown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (thrown && collision.gameObject.tag.Equals("Enemy"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<Enemy_Behavior>().TakeDamage(20);
            Object.Destroy(gameObject);
        }
        else if (thrown)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Object.Destroy(gameObject);
        }
    }

}
