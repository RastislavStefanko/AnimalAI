using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAI : MonoBehaviour {

    public InputsAI values;

    [HideInInspector]
    public List<AnimationCurve> valuesCurves = new List<AnimationCurve>();

	public string GetUtility()
    {
        float biggestUtility = 0;
        int currentUtility = 0;

        for(int i = 0; i < values.inputs.Count; i++)
        {
            if(valuesCurves[i].Evaluate(values.inputs[i].currentValue/ values.inputs[i].startValue) > biggestUtility)
            {
                biggestUtility = valuesCurves[i].Evaluate(values.inputs[i].currentValue / values.inputs[i].startValue);
                currentUtility = i;
            }
        }

        return values.inputs[currentUtility].name;
    }

    public void SetValue(string inputName)
    {
        for (int i = 0; i < values.inputs.Count; i++)
        {
            if(values.inputs[i].name == inputName)
            {
                values.inputs[i].currentValue = values.inputs[i].startValue;
            }
        }
    }

    public void SetValue(string inputName, float value)
    {
        for (int i = 0; i < values.inputs.Count; i++)
        {
            if (values.inputs[i].name == inputName)
            {
                values.inputs[i].currentValue = value;
            }
        }
    }

    public void PlusValue(string inputName, float value)
    {
        for (int i = 0; i < values.inputs.Count; i++)
        {
            if (values.inputs[i].name == inputName)
            {
                values.inputs[i].currentValue += value;
            }
        }
    }
}
