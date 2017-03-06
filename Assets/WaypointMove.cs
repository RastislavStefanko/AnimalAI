using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour {

    public float thrust;
    public float gravityValue;
    public Rigidbody rb;

    private float time;

    void Start()
    {
        time = 5;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityValue);
    }

    void Update()
    {
        if (time < 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public void push()
    {
        time = 3;
        int whatDirection = Random.Range(0, 3);

        switch (whatDirection)
        {
            case 0:
                rb.AddRelativeForce(Vector3.forward * thrust);
                break;
            case 1:
                rb.AddRelativeForce(Vector3.back * thrust);
                break;
            case 2:
                rb.AddRelativeForce(Vector3.left * thrust);
                break;
            case 3:
                rb.AddRelativeForce(Vector3.right * thrust);
                break;
            default:
                break;
        }
        
    }
}
