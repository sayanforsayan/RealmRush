using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages and tracks all active quests in the game.
/// Updates quests when events are triggered (kill, collect, explore).
/// </summary>
public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<BaseQuest> activeQuests;

    /// <summary>
    /// Call when enemy killed
    /// </summary>
    public void EnemyKilled() => UpdateQuest(QuestType.Kill);

    /// <summary>
    /// Call when Cube Collected
    /// </summary>
    public void ItemCollected() => UpdateQuest(QuestType.Fetch);

    /// <summary>
    /// Call when visit area
    /// </summary>
    public void AreaReached() => UpdateQuest(QuestType.Explore);

    /// <summary>
    /// Update all information
    /// </summary>
    /// <param name="type"></param>
    private void UpdateQuest(QuestType type)
    {
        string info = "";
        foreach (var quest in activeQuests)
        {
            if (quest.questType == type && !quest.IsComplete)
                quest.Progress();
            info += $"{quest.title} - {quest.currentUnit}/{quest.totalUnit}\n";
        }

        // Update UI
        UIManager.Instance.ShowResult(info);
        if (AllComplete())
            UIManager.Instance.ShowGameComplete();
    }

    /// <summary>
    /// Returns true if all  are completed.
    /// </summary>
    private bool AllComplete()
    {
        foreach (var q in activeQuests)
            if (!q.IsComplete) return false;
        return true;
    }
}
