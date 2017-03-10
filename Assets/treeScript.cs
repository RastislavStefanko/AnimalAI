using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour {

    public bool isActive = true;

	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "giraffe" || other.gameObject.tag == "group")
        {
            isActive = false;
        }
    }
	
}
