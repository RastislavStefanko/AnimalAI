using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaDetect : MonoBehaviour
{

    public GameObject group;
    public int countBound = 0;

    void OnTriggerEnter(Collider other)
    {
        if (transform.parent.tag == "group" && (other.tag == "giraffe" || other.tag == "group"))
        {
            countBound++;
        }

        if (transform.parent.tag != "group" && other.tag == "giraffe")
        {
            if (other.transform.parent == null)
            {
                GameObject tmp = Instantiate(group, transform.position, Quaternion.identity);

                transform.parent.SetParent(tmp.transform);
            }

            transform.parent.tag = "group";

            transform.GetComponent<SphereCollider>().radius *= 1.5f;

            countBound++;
        }

        if (transform.parent.tag != "group" && other.tag == "group")
        {
            transform.parent.SetParent(other.transform.parent);

            transform.parent.tag = "group";

            transform.GetComponent<SphereCollider>().radius *= 1.5f;

            countBound++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "group" || other.tag == "giraffe")
        {
            countBound--;
        }

        if (transform.parent.tag == "group" && countBound == 0)
        {
            transform.parent.SetParent(null);

            transform.parent.tag = "giraffe";

            transform.GetComponent<SphereCollider>().radius /= 1.5f;
        }

    }
}