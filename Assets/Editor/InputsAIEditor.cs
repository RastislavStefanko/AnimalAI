using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputsAI))]
public class InputsAIEditor : Editor {

    InputsAI m_Target;

    public override void OnInspectorGUI()
    {
        m_Target = (InputsAI)target;

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
            GUILayout.Label("Start Value", EditorStyles.label, GUILayout.Width(80));
            GUILayout.Label("Current Value", EditorStyles.label, GUILayout.Width(100));
        }
        GUILayout.EndHorizontal();

        for (int i = 0; i < m_Target.inputs.Count; ++i)
        {
            DrawState(i);
        }

        DrawAddStateButton();
    }

    void DrawState(int index)
    {
        if (index < 0 || index >= m_Target.inputs.Count)
        {
            return;
        }

        SerializedProperty listIterator = serializedObject.FindProperty("inputs");

        

        GUILayout.BeginHorizontal();
        {

            

            EditorGUI.BeginChangeCheck();
            string newName = GUILayout.TextField(m_Target.inputs[index].name, GUILayout.Width(80));
            float newStartValue = EditorGUILayout.FloatField("", m_Target.inputs[index].startValue, GUILayout.Width(80));
            float newCurrentValue = EditorGUILayout.FloatField("", m_Target.inputs[index].currentValue, GUILayout.Width(80));

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(m_Target, "Modify State");

                m_Target.inputs[index].name = newName;
                m_Target.inputs[index].startValue = newStartValue;
                m_Target.inputs[index].currentValue = newCurrentValue;

                EditorUtility.SetDirty(m_Target);
            }

            if (GUILayout.Button("Remove"))
            {

                    Undo.RecordObject(m_Target, "Delete State");
                    m_Target.inputs.RemoveAt(index);
                    EditorUtility.SetDirty(m_Target);
                
            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawAddStateButton()
    {
        if (GUILayout.Button("Add new Value", GUILayout.Height(30)))
        {
            Undo.RecordObject(m_Target, "Add new State");

            m_Target.inputs.Add(new InputAI());

            EditorUtility.SetDirty(m_Target);
        }
    }
}
