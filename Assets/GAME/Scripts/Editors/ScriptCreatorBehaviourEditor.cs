// Editor Script: Assets/Editor/ScriptCreatorBehaviourEditor.cs
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ScriptCreatorBehaviour bile�enine �zel inspector,
/// her entry i�in script olu�turma i�levini sa�lar.
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

        EditorGUILayout.LabelField("Script Olu�turma Ayarlar�", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_entriesProp, new GUIContent("Entries"), true);

        GUILayout.Space(8);
        if (GUILayout.Button("Toplu MonoBehaviour Script Olu�tur"))
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
                EditorUtility.DisplayDialog("Ba�ar�l�", "T�m ge�erli scriptler olu�turuldu!", "Tamam");
            }
            else
            {
                EditorUtility.DisplayDialog("Uyar�", "Ge�erli bir script ad� ve klas�r se�imi bulunamad�.", "Tamam");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateScript(string scriptName, string folderPath)
    {
        string filePath = Path.Combine(folderPath, scriptName + ".cs");
        if (File.Exists(filePath))
        {
            if (!EditorUtility.DisplayDialog("Uyar�", $"{scriptName}.cs zaten var. �zerine yaz�ls�n m�?", "Evet", "Hay�r"))
                return;
        }

        string template =
$@"using UnityEngine;

/// <summary>
/// {scriptName} MonoBehaviour s�n�f�.
/// </summary>
public class {scriptName} : MonoBehaviour
{{
    void Start() {{ }}
    void Update() {{ }}
}}";

        File.WriteAllText(filePath, template);
    }
}
