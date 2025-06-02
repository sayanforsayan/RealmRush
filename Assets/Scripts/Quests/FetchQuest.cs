using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Fetch Quest")]
public class FetchQuest : BaseQuest
{
    public override void Progress()
    {
        currentCount++;
    }
}
