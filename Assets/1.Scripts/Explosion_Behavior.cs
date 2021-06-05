using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);
    }
}
