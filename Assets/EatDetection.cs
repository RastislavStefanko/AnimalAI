using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatDetection : MonoBehaviour {

    public GameObject wholeObject;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("hit");
        if (collision.transform.tag == "leafs")
        {
            wholeObject.GetComponent<AI>().eat = true;
        }
    }

    /*void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "leafs")
        {
            wholeObject.GetComponent<AI>().eat = false;
        }
    }*/
}
