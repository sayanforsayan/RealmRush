using UnityEngine;

public enum QuestType { Fetch, Kill, Explore } //  enum for categories

/// <summary>
/// Base class of the Quest, store required information
/// </summary>
public abstract class BaseQuest : ScriptableObject
{
    public string title;
    public string description;
    public int receiveUnit;
    public int totalUnit;
    public int currentUnit;
    public QuestType questType;

    public bool IsComplete => currentUnit >= totalUnit;

    public abstract void Progress();
}
