using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI questText;
    public GameObject gameCompleteMessage;

    private void Awake() => Instance = this;

    private void Update()
    {
        questText.text = "";
        foreach (var quest in QuestManager.Instance.activeQuests)
        {
            questText.text += $"{quest.title} - {quest.currentCount}/{quest.goalCount}\n";
        }
    }

    public void ShowGameComplete() => gameCompleteMessage.SetActive(true);
}
