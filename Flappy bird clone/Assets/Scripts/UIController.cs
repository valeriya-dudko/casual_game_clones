using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentScoreText;
    [SerializeField]
    TextMeshProUGUI finalScoreText;
    [SerializeField]
    GameObject gameoverPanel;
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    Button replayButton;
    [SerializeField]
    Button playButton;

    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        replayButton.onClick.AddListener(ReplayOnClick);
        playButton.onClick.AddListener(PlayOnClick);
    }

    void ReplayOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlayOnClick()
    {
        startPanel.SetActive(false);
        GameController.Instance.StartGame();
    }

    public void ShowGameoverScreen(int finalScore)
    {
        currentScoreText.gameObject.SetActive(false);
        gameoverPanel.SetActive(true);
        finalScoreText.SetText(finalScore.ToString());
    }

    public void EnableCurrentScore()
    {
        currentScoreText.gameObject.SetActive(true);
    }

    public void ShowStartScreen()
    {
        startPanel.SetActive(true);
        currentScoreText.gameObject.SetActive(false);
    }

    public void UpdateCurrentScoreUI(int currScore)
    {
        currentScoreText.SetText(currScore.ToString());
    }

    
}
