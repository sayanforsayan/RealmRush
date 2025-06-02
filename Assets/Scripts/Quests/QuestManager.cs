using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<BaseQuest> activeQuests;

    private void Awake()
    {
        Instance = this;
    }

    public void EnemyKilled() => UpdateQuest(QuestType.Kill);
    public void ItemCollected() => UpdateQuest(QuestType.Fetch);
    public void AreaReached() => UpdateQuest(QuestType.Explore);

    private void UpdateQuest(QuestType type)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.questType == type && !quest.IsComplete)
                quest.Progress();
        }

        if (AllComplete())
            UIManager.Instance.ShowGameComplete();
    }

    private bool AllComplete()
    {
        foreach (var quest in activeQuests)
            if (!quest.IsComplete) return false;
        return true;
    }
}
