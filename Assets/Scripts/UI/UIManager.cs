using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Show information and Start, game over show
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject gameCompleteMessage, startPanel;
    [SerializeField] private Button startBtn, replayBtn;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameCompleteMessage.SetActive(false);
        startPanel.SetActive(true);
        startBtn.onClick.AddListener(GameStart);
    }

    // Show Updated information
    public void ShowResult(string getInfo)
    {
        questText.text = getInfo;
    }

    // Game over message
    public void ShowGameComplete()
    {
        gameCompleteMessage.SetActive(true);
    }

    // When  Start press
    private void GameStart()
    {
        startPanel.SetActive(false);
    }
}
