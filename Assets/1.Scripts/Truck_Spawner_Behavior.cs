using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Spawner_Behavior : MonoBehaviour
{

    //Used to wait to spawn a truck
    public float cooldown;

    //Used to compare to cooldown
    private float timer;

    //Used to activate the spawner
    private bool wasSeen;

    //Object to spawn
    public GameObject spawn;

    // Update is called once per frame
    void Update()
    {
        WasSeen();
        if (wasSeen)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Instantiate(spawn, transform.position, transform.rotation);
            }
        }
    }

    //Checks if the player saw the spawner
    void WasSeen()
    {
        wasSeen = true;
    }

}
