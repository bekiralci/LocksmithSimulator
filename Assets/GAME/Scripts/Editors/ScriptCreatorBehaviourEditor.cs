// Editor Script: Assets/Editor/ScriptCreatorBehaviourEditor.cs
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ScriptCreatorBehaviour bileþenine özel inspector,
/// her entry için script oluþturma iþlevini saðlar.
/// </summary>
[CustomEditor(typeof(ScriptCreatorBehaviour))]
public class ScriptCreatorBehaviourEditor : Editor
{
    private SerializedProperty _entriesProp;

    private void OnEnable()
    {
        _entriesProp = serializedObject.FindProperty("entries");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Script Oluþturma Ayarlarý", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_entriesProp, new GUIContent("Entries"), true);

        GUILayout.Space(8);
        if (GUILayout.Button("Toplu MonoBehaviour Script Oluþtur"))
        {
            var behaviour = (ScriptCreatorBehaviour)target;
            bool anyCreated = false;

            foreach (var entry in behaviour.entries)
            {
                if (string.IsNullOrWhiteSpace(entry.scriptName) || entry.targetFolder == null)
                    continue;

                string folderPath = AssetDatabase.GetAssetPath(entry.targetFolder);
                if (!AssetDatabase.IsValidFolder(folderPath))
                    continue;

                CreateScript(entry.scriptName.Trim(), folderPath);
                anyCreated = true;
            }

            if (anyCreated)
            {
                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("Baþarýlý", "Tüm geçerli scriptler oluþturuldu!", "Tamam");
            }
            else
            {
                EditorUtility.DisplayDialog("Uyarý", "Geçerli bir script adý ve klasör seçimi bulunamadý.", "Tamam");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateScript(string scriptName, string folderPath)
    {
        string filePath = Path.Combine(folderPath, scriptName + ".cs");
        if (File.Exists(filePath))
        {
            if (!EditorUtility.DisplayDialog("Uyarý", $"{scriptName}.cs zaten var. Üzerine yazýlsýn mý?", "Evet", "Hayýr"))
                return;
        }

        string template =
$@"using UnityEngine;

/// <summary>
/// {scriptName} MonoBehaviour sýnýfý.
/// </summary>
public class {scriptName} : MonoBehaviour
{{
    void Start() {{ }}
    void Update() {{ }}
}}";

        File.WriteAllText(filePath, template);
    }
}
