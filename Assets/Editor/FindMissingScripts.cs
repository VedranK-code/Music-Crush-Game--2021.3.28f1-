using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissingScriptsInScene()
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        int missingScriptCount = 0;

        foreach (GameObject go in objects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script found in GameObject: {go.name}", go);
                    missingScriptCount++;
                }
            }
        }

        Debug.Log($"Total missing scripts found: {missingScriptCount}");
    }
}
