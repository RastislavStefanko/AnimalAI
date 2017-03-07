using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsAI : MonoBehaviour {

    [HideInInspector]
    public List<InputAI> inputs = new List<InputAI>();

    void Start()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            inputs[i].currentValue = inputs[i].startValue;
        }
    }

    void Update()
    {
        for(int i = 0; i < inputs.Count; i++)
        {
            inputs[i].currentValue -= Time.deltaTime;
        }
    }
}
