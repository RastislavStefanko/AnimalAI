using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    private List<GameObject> trees = new List<GameObject>();
    private Dictionary<int,float> treeCounters = new Dictionary<int, float>();

    public float treeRespawn = 10;

    void Start()
    {
        foreach (Transform tree in transform)
        {
            trees.Add(tree.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < trees.Count; i++)
        {
            if (!trees[i].GetComponent<treeScript>().isActive)
            {
                trees[i].SetActive(false);
                if (!treeCounters.ContainsKey(i))
                {
                    treeCounters.Add(i, treeRespawn);
                }
            }
        }

        List<int> tmp = new List<int>(treeCounters.Keys);
        foreach (int key in tmp)
        {
            if(treeCounters[key] > 0)
            {
                treeCounters[key] -= Time.deltaTime;
            }
            else
            {
                treeCounters.Remove(key);
                trees[key].SetActive(true);
                trees[key].GetComponent<treeScript>().isActive = true;
            }            
        }
    }
}
