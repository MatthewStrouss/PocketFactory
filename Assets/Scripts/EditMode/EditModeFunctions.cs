#if (UNITY_EDITOR) 
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
public class EditModeFunctions : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Rotate Object"))
        {
            RotateObject(target as GameObject);
        }

        if (GUILayout.Button("Rotate Left"))
        {
            RotateObject(target as GameObject, 270);
        }

        if (GUILayout.Button("Rotate Up"))
        {
            RotateObject(target as GameObject, 180);
        }

        if (GUILayout.Button("Rotate Right"))
        {
            RotateObject(target as GameObject, 90);
        }

        if (GUILayout.Button("Rotate Down"))
        {
            RotateObject(target as GameObject, 0);
        }

        if (GUILayout.Button("Toggle Active"))
        {
            ToggleActive(target as GameObject);
        }
    }

    private void RotateObject(GameObject target)
    {
        (target as GameObject).transform.Rotate(0f, 0f, 90f);
    }

    private void RotateObject(GameObject target, float rotation)
    {
        (target as GameObject).transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void ToggleActive(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }
}
#endif
