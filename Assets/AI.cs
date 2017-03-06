using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    public GameObject Player;

    public GameObject toGoObject;
    private NavMeshAgent agent;
    public GameObject[] waypoints;

    private Plane[] planes;
    public Camera sight;

    private GameObject currentDestination;
    public float hunger = 100;

    private Animator anim;
    public bool eat { get; set; }
    public float eatTime = 4;

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
            if (hunger < 30)
            {
                currentDestination = gameObject;
                if (currentDestination.tag != "leafs")
                {
                    FindFood();
                }

                if (currentDestination.tag == "leafs" && eat)
                {
                    Eat();
                }
            }
            else
            {
                anim.SetBool("eat", false);
                currentDestination = waypoints[0];
            }

        }
        else
        {
            waitUntilDone -= Time.deltaTime;
        }
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
            foreach (GameObject leaf in leafs)
            {
                if (GeometryUtility.TestPlanesAABB(planes, leaf.GetComponent<Collider>().bounds))
                {
                    currentDestination = leaf;
                }
            }

            if(currentDestination.tag != "leafs")
            {
                transform.Rotate(0,-1, 0);
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
        Debug.Log("hit");
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
    }
}
