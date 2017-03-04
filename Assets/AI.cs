using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject Player;
    float MoveSpeed = 4;
    float MaxDist = 10;
    float MinDist = 5;




    void Start()
    {

    }

    void Update()
    {
        //transform.LookAt(Player.transform, Vector3.left);
        transform.LookAt(Vector3.zero);
        /*if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }*/
    }
}
