using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeEat : MonoBehaviour {

    public void Eat()
    {
        /*currentDestination = gameObject;
        hunger = 100;
        anim.SetBool("eat", true);
        waitUntilDone = eatTime;
        eat = false;*/
    }

    public void FindFood()
    {
        /*GameObject[] leafs = GameObject.FindGameObjectsWithTag("leafs");
        if (currentDestination.tag != "leafs")
        {
            currentDestination = gameObject;
            foreach (GameObject leaf in leafs)
            {
                if (GeometryUtility.TestPlanesAABB(planes, leaf.GetComponent<Collider>().bounds))
                {
                    currentDestination = leaf;
                }
            }

            if (currentDestination.tag != "leafs")
            {
                transform.Rotate(0, -1, 0);
            }
        }
        else
        {

            foreach (GameObject leaf in leafs)
            {
                if (GeometryUtility.TestPlanesAABB(planes, leaf.GetComponent<Collider>().bounds))
                {

                    if (Vector3.Distance(currentDestination.transform.position, transform.position) >= Vector3.Distance(leaf.transform.position, transform.position))
                    {
                        currentDestination = leaf;
                    }
                }
            }

        }*/

    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "leafs")
        {
            eat = true;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "leafs")
        {
            eat = false;
        }

    }*/
}
