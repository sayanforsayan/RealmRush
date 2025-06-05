using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Advanced Unity Editor Window to create quests individually or from JSON. Also can edit existing quest
/// N.B : current unit not add it will take value on runtime means gameplay time
/// </summary>
public class AdvancedQuestEditorWindow : EditorWindow
{
    private string questTitle = "New Quest";
    private string description = "";
    private int totalUnit = 1;
    private int reward = 1;
    private QuestType questType = QuestType.Fetch;

    private string assetPath = "Assets/ScriptableObjects/Quests/";
    private TextAsset jsonFile;

    private BaseQuest selectedQuestToEdit = null;

    [MenuItem("Tools/Advanced Quest Creator")]
    public static void ShowWindow()
    {
        GetWindow<AdvancedQuestEditorWindow>("Advanced Quest Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Manual Quest Creator / Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        selectedQuestToEdit = (BaseQuest)EditorGUILayout.ObjectField("Edit Existing Quest", selectedQuestToEdit, typeof(BaseQuest), false);

        if (selectedQuestToEdit != null)
        {
            // Load data into fields
            questTitle = selectedQuestToEdit.title;
            description = selectedQuestToEdit.description;
            totalUnit = selectedQuestToEdit.totalUnit;
            reward = selectedQuestToEdit.receiveUnit;
            questType = selectedQuestToEdit.questType;

            EditorGUILayout.HelpBox("Editing existing quest. Click 'Update Quest' to save changes.", MessageType.Info);
        }

        // Quest Fields as same as BaseQuest given variable
        questTitle = EditorGUILayout.TextField("Title", questTitle);
        description = EditorGUILayout.TextField("Description", description);
        totalUnit = EditorGUILayout.IntField("Goal Count", totalUnit);
        reward = EditorGUILayout.IntField("Reward", reward);
        questType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", questType);

        // Give message if placeholder empty or less than 1
        if (totalUnit < 1) EditorGUILayout.HelpBox("Goal Count must be ≥ 1", MessageType.Error);
        if (reward < 1) EditorGUILayout.HelpBox("Reward should be ≥ 1", MessageType.Warning);
        if (string.IsNullOrEmpty(questTitle)) EditorGUILayout.HelpBox("Title cannot be empty", MessageType.Error);
        if (string.IsNullOrEmpty(description)) EditorGUILayout.HelpBox("Description is required", MessageType.Info);

        // Disable Button if not give required data in placeholder
        EditorGUI.BeginDisabledGroup(string.IsNullOrWhiteSpace(questTitle) || totalUnit < 1);

        if (selectedQuestToEdit == null)
        {
            if (GUILayout.Button("Create Quest"))
            {
                CreateQuestAsset(questTitle, description, totalUnit, reward, questType);
            }
        }
        else
        {
            if (GUILayout.Button("Update Quest"))
            {
                UpdateQuestAsset(selectedQuestToEdit, questTitle, description, totalUnit, reward, questType);
            }
        }

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(20);
        GUILayout.Label("Bulk Quest Import (from JSON)", EditorStyles.boldLabel);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField("JSON File", jsonFile, typeof(TextAsset), false);

        if (jsonFile != null)
        {
            if (GUILayout.Button("Import Quests from JSON"))
            {
                ImportFromJson(jsonFile.text);
            }
        }
    }

    /// <summary>
    /// Create Scriptable Objects according to given data
    /// </summary>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    /// <param name="goalCount"></param>
    /// <param name="reward"></param>
    /// <param name="type"></param>
    private void CreateQuestAsset(string title, string desc, int goalCount, int reward, QuestType type, bool isFromJson = false)
    {
        BaseQuest newQuest = CreateQuestInstance(type);
        if (newQuest != null)
        {
            newQuest.title = title;
            newQuest.description = desc;
            newQuest.totalUnit = goalCount;
            newQuest.receiveUnit = reward;
            newQuest.questType = type;

            string sanitizedTitle = title.Replace(" ", "_");
            string fullPath = assetPath + sanitizedTitle + ".asset";
            AssetDatabase.CreateAsset(newQuest, fullPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newQuest;
            if (!isFromJson)
                EditorUtility.DisplayDialog("Quest Created", $"Quest '{questTitle}' created successfully!", "OK");
        }
    }

    /// <summary>
    /// Update Existing quest
    /// </summary>
    /// <param name="quest"></param>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    /// <param name="goalCount"></param>
    /// <param name="reward"></param>
    /// <param name="type"></param>
    private void UpdateQuestAsset(BaseQuest quest, string title, string desc, int goalCount, int reward, QuestType type)
    {
        quest.title = title;
        quest.description = desc;
        quest.totalUnit = goalCount;
        quest.receiveUnit = reward;
        quest.questType = type;

        EditorUtility.SetDirty(quest);
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Quest Updated", $"Quest '{quest.title}' updated successfully!", "OK");
    }

    private BaseQuest CreateQuestInstance(QuestType type)
    {
        switch (type)
        {
            case QuestType.Fetch: return ScriptableObject.CreateInstance<FetchQuest>();
            case QuestType.Kill: return ScriptableObject.CreateInstance<KillQuest>();
            case QuestType.Explore: return ScriptableObject.CreateInstance<ExploreQuest>();
            default: return null;
        }
    }

    /// <summary>
    /// Extract Data from json
    /// </summary>
    /// <param name="jsonText"></param>
    private void ImportFromJson(string jsonText)
    {
        QuestDataWrapper wrapper = JsonUtility.FromJson<QuestDataWrapper>(jsonText);

        if (wrapper == null || wrapper.Items == null || wrapper.Items.Length == 0)
        {
            EditorUtility.DisplayDialog("Import Failed", "The JSON file is empty or incorrectly formatted.", "OK");
            return;
        }

        int created = 0;

        foreach (var q in wrapper.Items)
        {
            if (IsValid(q))
            {
                CreateQuestAsset(q.title, q.description, q.goalCount, q.reward, q.questType, true);
                created++;
            }
            else
            {
                Debug.LogWarning($"Skipped invalid quest: {q.title}");
            }
        }

        EditorUtility.DisplayDialog("Import Complete", $"Successfully created {created} quests from JSON.", "OK");
        AssetDatabase.Refresh();
    }

    private bool IsValid(QuestData quest)
    {
        return !string.IsNullOrWhiteSpace(quest.title)
            && !string.IsNullOrWhiteSpace(quest.description)
            && quest.goalCount > 0
            && quest.reward > 0;
    }

    /// <summary>
    ///  Use same signature according to json format
    /// </summary>
    [Serializable]
    public class QuestData
    {
        public string title;
        public string description;
        public int goalCount;
        public int reward;
        public QuestType questType;
    }

    [Serializable]
    public class QuestDataWrapper
    {
        public QuestData[] Items;
    }
}
