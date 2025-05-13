// Runtime Script: Assets/Scripts/EditorTools/ScriptCreatorBehaviour.cs
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Inspector �zerinden farkl� klas�rlere, farkl� isimlerle
/// MonoBehaviour scriptleri olu�turmay� sa�lar.
/// </summary>
public class ScriptCreatorBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class ScriptEntry
    {
        [Tooltip("Olu�turulacak script dosya ad� (uzant�s�z)")]
        public string scriptName = "NewScript";

        [Tooltip("Olu�turulacak dosyan�n yer alaca�� klas�r� se�in (Assets/...)")]
        public DefaultAsset targetFolder;
    }

    [Tooltip("Her bir ��e i�in farkl� script ad� ve klas�r se�imi yap�n.")]
    public List<ScriptEntry> entries = new List<ScriptEntry>
    {
        new ScriptEntry()
    };
}