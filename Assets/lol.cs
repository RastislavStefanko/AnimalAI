using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
    }
}
