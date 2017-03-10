using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBehavior : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if(getNumberOfObjects() == 0)
        {
            Destroy(gameObject);
        }
    }

    public int getNumberOfObjects()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            count++;
        }

        return count;
    }
}