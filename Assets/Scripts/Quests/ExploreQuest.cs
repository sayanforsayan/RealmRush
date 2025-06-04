using UnityEngine;

/// <summary>
/// Information for area
/// </summary>
[CreateAssetMenu(menuName = "Quests/Explore Quest")]
public class ExploreQuest : BaseQuest
{
    public override void Progress()
    {
        currentUnit += receiveUnit;
        Debug.Log("Reached Zone");
    }
}
