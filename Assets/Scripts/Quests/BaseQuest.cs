using UnityEngine;

public enum QuestType { Fetch, Kill, Explore }

public abstract class BaseQuest : ScriptableObject
{
    public string title;
    public string description;
    public int goalCount;
    public int currentCount;
    public string reward;
    public QuestType questType;

    public bool IsComplete => currentCount >= goalCount;

    public abstract void Progress();
}
