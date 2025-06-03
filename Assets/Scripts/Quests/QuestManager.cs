using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<BaseQuest> activeQuests;

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += EnemyKilled;
        GameEvents.OnItemCollected += ItemCollected;
        GameEvents.OnAreaReached += AreaReached;
        GameEvents.OnResetCall += ResetData;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= EnemyKilled;
        GameEvents.OnItemCollected -= ItemCollected;
        GameEvents.OnAreaReached -= AreaReached;
        GameEvents.OnResetCall -= ResetData;
    }

    void EnemyKilled()
    {
        UpdateQuest(QuestType.Kill);
    }

    void ItemCollected()
    {
        UpdateQuest(QuestType.Fetch);
    }
    void AreaReached()
    {
        UpdateQuest(QuestType.Explore);
    }

    void UpdateQuest(QuestType type)
    {
        string info = "";
        foreach (var quest in activeQuests)
        {
            if (quest.questType == type && !quest.IsComplete)
                quest.Progress();
            info += $"{quest.title} - {quest.currentCount}/{quest.goalCount}\n";
        }
        UIManager.Instance.ShowResult(info);
        if (AllComplete())
            UIManager.Instance.ShowGameComplete();
    }

    bool AllComplete()
    {
        foreach (var acQ in activeQuests)
            if (!acQ.IsComplete) return false;
        return true;
    }

    private void ResetData()
    {
        string info = "";
        foreach (var quest in activeQuests)
        {
            quest.currentCount = 0;
            info += $"{quest.title} - {quest.currentCount}/{quest.goalCount}\n";
        }
        UIManager.Instance.ShowResult(info);
    }
}
