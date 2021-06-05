using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Behavior : MonoBehaviour
{

    //public float speed = 10f;

    private Rigidbody2D rgBody;

    private BoxCollider2D box;

    private bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed)
        {
            Drive();
        }
        
    }

    void Drive()
    {
        Vector3 movement = new Vector3(-1f, 0f, 0f);
        transform.position += movement * Time.deltaTime;
    }

    public void Grabbed(GameObject obj)
    {
        isGrabbed = true;

        gameObject.transform.parent = obj.transform;

        float parentSize = obj.GetComponent<BoxCollider2D>().bounds.extents.y * 2;

        transform.localPosition = new Vector3(0f, 0f, 0f);

        Vector3 movement = new Vector3(0.3f, parentSize, 0f);
        transform.localPosition += movement;

        rgBody.isKinematic = true;

    }

    public void Thrown(Vector2 force)
    {
        rgBody.isKinematic = false;

        rgBody.AddForce(force);

        GetComponent<Truck_On_Collision>().WasThrown();

        gameObject.layer = 9;
    }

}
