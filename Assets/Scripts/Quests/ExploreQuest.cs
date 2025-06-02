using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Explore Quest")]
public class ExploreQuest : BaseQuest
{
    public override void Progress()
    {
        currentCount = goalCount;
    }
}
