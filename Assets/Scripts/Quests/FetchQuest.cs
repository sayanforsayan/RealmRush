using UnityEngine;

/// <summary>
/// Information for Cube Health
/// </summary>
[CreateAssetMenu(menuName = "Quests/Fetch Quest")]
public class FetchQuest : BaseQuest
{
    public override void Progress()
    {
        currentUnit += receiveUnit;
        Debug.Log("Collect Cube");
    }
}
