using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AddNecessaryTag
{
    private static readonly string[] DefaultTags = { "Untagged", "Respawn", "Finish", "EditorOnly", "MainCamera", "Player", "GameController" };

    // Automaticamente chiama AddNecessaryTags() quando il plugin viene aggiunto al progetto
    [InitializeOnLoadMethod]
    public static void AddNecessaryTags()
    {
        if (!UnityEditorInternal.InternalEditorUtility.tags.Contains("Collection"))
        {
            // Carica il TagManager asset
            var tagManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
            if (tagManagerAsset != null && tagManagerAsset.Length > 0)
            {
                SerializedObject tagManager = new SerializedObject(tagManagerAsset[0]);
                SerializedProperty tagsProp = tagManager.FindProperty("tags");

                // Verifica se ci sono tag oltre a quelli di default
                bool hasCustomTags = tagsProp.arraySize > DefaultTags.Length;

                bool addToEnd = false;

                if (hasCustomTags)
                {
                    // Mostra una finestra di dialogo per chiedere dove aggiungere il tag
                    if (EditorUtility.DisplayDialog("Tag esistente",
                        "Sono già presenti dei tag oltre a quelli di default. Vuoi aggiungere il tag 'Collection' all'inizio o alla fine della lista?",
                        "Inizio", "Fine"))
                    {
                        addToEnd = false;
                    }
                    else
                    {
                        addToEnd = true;
                    }
                }

                // Aggiungi il tag "Collection"
                AddTag(tagsProp, "Collection", addToEnd);

                tagManager.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogError("TagManager asset non trovato. Verifica il percorso.");
            }
        }
    }

    private static void AddTag(SerializedProperty tagsProp, string newTag, bool addToEnd)
    {
        bool found = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            if (addToEnd)
            {
                tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
                SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
                newTagProp.stringValue = newTag;
            }
            else
            {
                tagsProp.InsertArrayElementAtIndex(0);
                SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
                newTagProp.stringValue = newTag;
            }
        }
    }
}