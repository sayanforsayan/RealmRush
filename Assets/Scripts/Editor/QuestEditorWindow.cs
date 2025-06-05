using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Unity Editor window to create and edit Quest ScriptableObjects
/// N.B : current unit not add it will take value on runtime means gameplay time
/// </summary>
public class QuestEditorWindow : EditorWindow
{
    private string questTitle = "New Quest";
    private string description = "";
    private int totalUnit = 1;
    private int reward = 1;

    private QuestType questType = QuestType.Fetch;

    private string assetPath = "Assets/ScriptableObjects/Quests/";

    [MenuItem("Tools/Quest Creator")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditorWindow>("Quest Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Quest Creator Tool", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Quest Fields as same as BaseQuest given variable
        questTitle = EditorGUILayout.TextField("Title", questTitle);
        description = EditorGUILayout.TextField("Description", description);
        totalUnit = EditorGUILayout.IntField("Goal Count", totalUnit);
        reward = EditorGUILayout.IntField("Reward", reward);

        questType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", questType);
        EditorGUILayout.Space();

        // Give message if placeholder empty or less than 1
        if (totalUnit < 1) EditorGUILayout.HelpBox("Goal Count must be ≥ 1", MessageType.Error);
        if (reward < 1) EditorGUILayout.HelpBox("Player can get reward", MessageType.Warning);
        if (string.IsNullOrEmpty(questTitle)) EditorGUILayout.HelpBox("Title cannot be empty", MessageType.Error);
        if (String.IsNullOrEmpty(description)) EditorGUILayout.HelpBox("Add description", MessageType.Info);

        // Disable Button if not give required data in placeholder
        EditorGUI.BeginDisabledGroup(string.IsNullOrWhiteSpace(questTitle) || totalUnit < 1);
        if (GUILayout.Button("Create Quest"))
        {
            CreateQuestAsset();
        }
    }

    /// <summary>
    /// Create Scriptable Objects according to given data 
    /// </summary>
    private void CreateQuestAsset()
    {
        if (totalUnit < 1 || string.IsNullOrEmpty(questTitle)) return;

        BaseQuest newQuest = null;

        switch (questType)
        {
            case QuestType.Fetch:
                newQuest = ScriptableObject.CreateInstance<FetchQuest>();
                break;
            case QuestType.Kill:
                newQuest = ScriptableObject.CreateInstance<KillQuest>();
                break;
            case QuestType.Explore:
                newQuest = ScriptableObject.CreateInstance<ExploreQuest>();
                break;
        }

        // Assign given value and create SO file
        if (newQuest != null)
        {
            newQuest.title = questTitle;
            newQuest.description = description;
            newQuest.totalUnit = totalUnit;
            newQuest.receiveUnit = reward;
            newQuest.questType = questType;

            string fullPath = assetPath + questTitle.Replace(" ", "_") + ".asset";
            AssetDatabase.CreateAsset(newQuest, fullPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newQuest;

            // Final Popup Message
            EditorUtility.DisplayDialog("Quest Created", $"Quest '{questTitle}' created successfully!", "OK");
        }
    }
}
