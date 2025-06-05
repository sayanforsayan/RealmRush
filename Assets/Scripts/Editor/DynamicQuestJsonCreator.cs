using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Tool to dynamically create multiple quests manually and export them to JSON
/// </summary>
public class DynamicQuestJsonCreator : EditorWindow
{
    private int questCount = 0;
    private List<QuestData> questList = new List<QuestData>();
    private Vector2 scroll;

    private string exportFolder = "Assets/Data";
    private string fileName = "ManualQuestsExport";

    [MenuItem("Tools/Dynamic Quest JSON Creator")]
    public static void ShowWindow()
    {
        GetWindow<DynamicQuestJsonCreator>("Dynamic Quest Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Manual Quest Entry & Export", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        questCount = EditorGUILayout.IntField("How many quests to create?", questCount);

        if (questCount < 0)
            questCount = 0;

        if (questCount == 0)
        {
            EditorGUILayout.HelpBox("Enter a number greater than 0.", MessageType.Warning);
            return;
        }

        if (questList.Count != questCount)
        {
            ResizeQuestList(questCount);
        }

        scroll = EditorGUILayout.BeginScrollView(scroll);
        for (int i = 0; i < questList.Count; i++)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Quest {i + 1}", EditorStyles.boldLabel);
            questList[i].title = EditorGUILayout.TextField("Title", questList[i].title);
            questList[i].description = EditorGUILayout.TextField("Description", questList[i].description);
            questList[i].goalCount = EditorGUILayout.IntField("Goal Count", questList[i].goalCount);
            questList[i].reward = EditorGUILayout.IntField("Reward", questList[i].reward);
            questList[i].questType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", questList[i].questType);

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(15);
        GUILayout.Label("Export Settings", EditorStyles.boldLabel);

        fileName = EditorGUILayout.TextField("File Name (no .json needed)", fileName);
        exportFolder = EditorGUILayout.TextField("Export Folder (under Assets)", exportFolder);

        // Handle final export path
        string finalFileName = fileName.EndsWith(".json") ? fileName : fileName + ".json";
        string fullPath = Path.Combine(exportFolder, finalFileName);
        fullPath = fullPath.Replace("\\", "/");

        EditorGUILayout.LabelField("Final Path:", fullPath);

        bool pathIsValid = fullPath.StartsWith("Assets");
        bool fileExists = File.Exists(fullPath);

        if (!pathIsValid)
        {
            EditorGUILayout.HelpBox("Export path must be inside the Assets folder.", MessageType.Error);
        }

        if (fileExists)
        {
            EditorGUILayout.HelpBox("Warning: A file with this name already exists!", MessageType.Warning);
        }

        if (!IsAllDataValid() || !pathIsValid)
        {
            EditorGUILayout.HelpBox("Please fill all fields correctly. Goal Count and Reward must be > 0.", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Export to JSON"))
        {
            ExportToJson(fullPath);
        }

        GUI.enabled = true;
    }

    private void ResizeQuestList(int newSize)
    {
        while (questList.Count < newSize)
        {
            questList.Add(new QuestData());
        }

        while (questList.Count > newSize)
        {
            questList.RemoveAt(questList.Count - 1);
        }
    }

    private bool IsAllDataValid()
    {
        foreach (var quest in questList)
        {
            if (string.IsNullOrWhiteSpace(quest.title)) return false;
            if (string.IsNullOrWhiteSpace(quest.description)) return false;
            if (quest.goalCount < 1) return false;
            if (quest.reward < 1) return false;
        }
        return true;
    }

    private void ExportToJson(string path)
    {
        // Ensure directory exists
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonHelper.ToJson(questList.ToArray(), true);
        File.WriteAllText(path, json);
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Export Successful", $"Exported {questList.Count} quests to:\n{path}", "OK");
    }

    [System.Serializable]
    public class QuestData
    {
        public string title;
        public string description;
        public int goalCount;
        public int reward;
        public QuestType questType;
    }

    public static class JsonHelper
    {
        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T> { Items = array };
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
