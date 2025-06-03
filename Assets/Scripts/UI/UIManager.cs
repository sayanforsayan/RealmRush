using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject gameCompleteMessage;
    [SerializeField] private Button startBtn, replayBtn;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        startBtn.onClick.AddListener(GameStart);
        replayBtn.onClick.AddListener(GameStart);
    }

    public void ShowResult(string getInfo)
    {
        questText.text = getInfo;
    }

    public void ShowGameComplete()
    {
        gameCompleteMessage.SetActive(true);
    }

    private void GameStart()
    {
        GameEvents.ResetData();
    }
}
