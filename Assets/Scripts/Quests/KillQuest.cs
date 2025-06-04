using UnityEngine;

/// <summary>
/// Information for enemy killed
/// </summary>
[CreateAssetMenu(menuName = "Quests/Kill Quest")]
public class KillQuest : BaseQuest
{
    public override void Progress()
    {
        currentUnit += receiveUnit;
        Debug.Log("Killed Enemy");
    }
}
