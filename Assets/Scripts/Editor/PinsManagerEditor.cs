using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PinsManager))]
public class InspectorButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PinsManager example = (PinsManager)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Pins"))
        {
            example.CreatePins();
        }

        if (GUILayout.Button("Remove Pins"))
        {
            example.RemovePins();
        }

        GUILayout.EndHorizontal();
    }
}
