using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public GameObject Player;

    public GameObject toGoObject;
    private NavMeshAgent agent;
    public GameObject[] waypoints;

    private Plane[] planes;
    public Camera sight;

    private GameObject currentDestination;
    public float hunger = 100;
    public float thirst = 100;

    private Animator anim;
    public bool eat { get; set; }
    public float eatTime = 4;

    public bool drink { get; set; }
    public float drinkTime;

    public float waitUntilDone = 0;

    void Start()
    {
        eat = false;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentDestination = gameObject;
    }

    void Update()
    {
        agent.destination = currentDestination.transform.position;

        if (waitUntilDone <= 0)
        {
            planes = GeometryUtility.CalculateFrustumPlanes(sight);
            if (hunger < 0)
            {
                if (!eat)
                {
                    FindFood();
                }
                else
                {
                    Eat();
                }
            }
            else if (thirst < 0)
            {
                currentDestination = gameObject;
                if (currentDestination.tag != "water")
                {
                    FindWater();
                }

                if (currentDestination.tag == "water" && drink)
                {
                    DrinkWater();
                }
            }
            else
            {
                anim.SetBool("drink", false);
                anim.SetBool("eat", false);
                currentDestination = waypoints[0];
            }

        }
        else
        {
            waitUntilDone -= Time.deltaTime;
        }
        thirst -= Time.deltaTime * 0.1f;
        hunger -= Time.deltaTime;
    }

    void Eat()
    {
        currentDestination = gameObject;
        hunger = 100;
        anim.SetBool("eat", true);
        waitUntilDone = eatTime;
        eat = false;
    }

    void FindFood()
    {
        GameObject[] leafs = GameObject.FindGameObjectsWithTag("leafs");
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

        }

    }

    void DrinkWater()
    {
        currentDestination = gameObject;
        thirst = 100;
        anim.SetBool("drink", true);
        waitUntilDone = drinkTime;
        drink = false;
    }

    void FindWater()
    {
        GameObject[] waterSources = GameObject.FindGameObjectsWithTag("water");
        foreach (GameObject water in waterSources)
        {
            if (GeometryUtility.TestPlanesAABB(planes, water.GetComponent<Collider>().bounds))
            {
                currentDestination = water;
            }
        }

        if (currentDestination.tag != "water")
        {
            transform.Rotate(0, -1, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "toGo")
        {
            toGoObject.GetComponent<WaypointMove>().push();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "leafs")
        {
            eat = true;
        }

        if (collision.transform.tag == "water")
        {
            drink = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "leafs")
        {
            eat = false;
        }

        if (collision.transform.tag == "water")
        {
            drink = false;
        }
    }
}
