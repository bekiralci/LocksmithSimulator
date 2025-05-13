// Runtime Script: Assets/Scripts/EditorTools/ScriptCreatorBehaviour.cs
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Inspector üzerinden farklý klasörlere, farklý isimlerle
/// MonoBehaviour scriptleri oluþturmayý saðlar.
/// </summary>
public class ScriptCreatorBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class ScriptEntry
    {
        [Tooltip("Oluþturulacak script dosya adý (uzantýsýz)")]
        public string scriptName = "NewScript";

        [Tooltip("Oluþturulacak dosyanýn yer alacaðý klasörü seçin (Assets/...)")]
        public DefaultAsset targetFolder;
    }

    [Tooltip("Her bir öðe için farklý script adý ve klasör seçimi yapýn.")]
    public List<ScriptEntry> entries = new List<ScriptEntry>
    {
        new ScriptEntry()
    };
}