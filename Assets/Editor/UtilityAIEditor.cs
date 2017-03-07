using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UtilityAI))]
public class UtilityAIEditor : Editor {

    UtilityAI m_Target;

    private int counter = 0;

    public override void OnInspectorGUI()
    {
        m_Target = (UtilityAI)target;

        DrawDefaultInspector();
        DrawStatesInspector();
    }

    void DrawStatesInspector()
    {
        GUILayout.Space(5);
        GUILayout.Label("Values", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Name", EditorStyles.label, GUILayout.Width(80));
            GUILayout.Label("Utility Curve", EditorStyles.label, GUILayout.Width(80));
        }
        GUILayout.EndHorizontal();

        if(counter < m_Target.values.inputs.Count)
        {
            counter++;
            m_Target.valuesCurves.Add(new AnimationCurve());
        }

        if(counter > m_Target.values.inputs.Count)
        {
            Undo.RecordObject(m_Target, "Delete State");
            m_Target.valuesCurves.RemoveAt(counter-1);
            EditorUtility.SetDirty(m_Target);
            counter--;
        }

        for (int i = 0; i < m_Target.values.inputs.Count; ++i)
        {
            DrawState(i);
        }

    }

    void DrawState(int index)
    {
        if (index < 0 || index >= m_Target.values.inputs.Count)
        {
            return;
        }

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(m_Target.values.inputs[index].name, EditorStyles.label, GUILayout.Width(80));

            EditorGUI.BeginChangeCheck();

            AnimationCurve newCurve = EditorGUILayout.CurveField(m_Target.valuesCurves[index]);
            

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(m_Target, "Modify State");

                m_Target.valuesCurves[index] = newCurve;

                EditorUtility.SetDirty(m_Target);
            }

        }
        GUILayout.EndHorizontal();
    }

}
