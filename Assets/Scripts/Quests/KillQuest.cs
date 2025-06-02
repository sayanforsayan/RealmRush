using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Kill Quest")]
public class KillQuest : BaseQuest
{
    public override void Progress()
    {
        currentCount++;
    }
}
