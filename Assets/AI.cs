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

    public float energy = 100;

    public UtilityAI giraffeAI;


    private float findWaterProblem;

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
            anim.SetBool("sleep", false);
            anim.SetBool("eat", false);
            anim.SetBool("drink", false);
            planes = GeometryUtility.CalculateFrustumPlanes(sight);
            switch (giraffeAI.GetUtility())
            {
                case "hunger":
                    if (!eat)
                    {
                        FindFood();
                    }
                    else
                    {
                        Eat();
                    }
                    break;
                case "thirst":
                    eat = false;

                    currentDestination = gameObject;
                    if (currentDestination.tag != "water")
                    {
                        FindWater();
                    }

                    if (currentDestination.tag == "water" && drink)
                    {
                        DrinkWater();
                    }
                    break;
                case "sleep":
                    eat = false;

                    giraffeAI.SetValue("sleep");
                    giraffeAI.SetValue("walk");
                    waitUntilDone = 30;
                    currentDestination = gameObject;
                    anim.SetBool("sleep", true);
                    break;
                case "findGroup":
                    eat = false;

                    anim.SetBool("sleep", false);
                    anim.SetBool("drink", false);
                    anim.SetBool("eat", false);
                    FindGroup();
                    break;
                default:
                    break;
            }

        }
        else
        {
            waitUntilDone -= Time.deltaTime;
        }

        if (gameObject.tag == "group")
        {
            giraffeAI.SetValue("findGroup", 1);
        }
        else
        {
            giraffeAI.SetValue("findGroup", 0);
        }
    }

    public void FindGroup()
    {
        GameObject[] groups = GameObject.FindGameObjectsWithTag("group");
        GameObject[] giraffes = GameObject.FindGameObjectsWithTag("giraffe");
        bool isSee = false;


        if (currentDestination.tag != "group" || currentDestination.tag != "giraffe")
        {
            currentDestination = gameObject;
            foreach (GameObject group in groups)
            {
                if (GeometryUtility.TestPlanesAABB(planes, group.GetComponent<Collider>().bounds))
                {
                    currentDestination = group;
                    isSee = true;
                }
            }


            foreach (GameObject giraffe in giraffes)
            {
                if (GeometryUtility.TestPlanesAABB(planes, giraffe.GetComponent<Collider>().bounds))
                {
                    currentDestination = giraffe;
                    isSee = true;
                }
            }

            
            if (!isSee)
            {
                transform.Rotate(0, -1 * Time.timeScale, 0);
            }
        }

        if (currentDestination.tag == "group" || currentDestination.tag == "giraffe")
        {
            foreach (GameObject group in groups)
            {
                if (GeometryUtility.TestPlanesAABB(planes, group.GetComponent<Collider>().bounds))
                {

                    if (Vector3.Distance(currentDestination.transform.position, transform.position) >= Vector3.Distance(group.transform.position, transform.position))
                    {
                        currentDestination = group;
                    }
                }
            }

            foreach (GameObject giraffe in giraffes)
            {
                if (GeometryUtility.TestPlanesAABB(planes, giraffe.GetComponent<Collider>().bounds))
                {

                    if (Vector3.Distance(currentDestination.transform.position, transform.position) >= Vector3.Distance(giraffe.transform.position, transform.position))
                    {
                        currentDestination = giraffe;
                    }
                }
            }
        }

    }

    public void Eat()
    {
        currentDestination = gameObject;
        giraffeAI.SetValue("hunger");
        giraffeAI.PlusValue("walk", 140);
        anim.SetBool("eat", true);
        waitUntilDone = eatTime;
        eat = false;
    }

    public void FindFood()
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
                transform.Rotate(0, -1 * Time.timeScale, 0);
            }
        }
        else
        {

            foreach (GameObject leaf in leafs)
            {
                if (GeometryUtility.TestPlanesAABB(planes, leaf.GetComponent<Collider>().bounds))
                {

                    if (Vector3.Distance(currentDestination.transform.position, transform.position) >= Vector3.Distance(leaf.transform.position, transform.position) || currentDestination.active == false)
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
        giraffeAI.SetValue("thirst");
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
            transform.Rotate(0, -1 * Time.timeScale, 0);
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

}
