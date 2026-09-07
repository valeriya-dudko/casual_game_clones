using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject canvas;
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI finalScoreText;
    TextMeshProUGUI bestScoreText;
    GameObject bestScoreToggle;
    GameObject gameoverPanel;
    GameObject startPanel;
    Button replayButton;
    Button playButton;

    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void InitUIObjects()
    {
        canvas = GameObject.Find("Canvas");

        startPanel = canvas.transform.Find("StartPanel").gameObject;
        gameoverPanel = canvas.transform.Find("GameoverPanel").gameObject;
        currentScoreText = canvas.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        finalScoreText = gameoverPanel.transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        playButton = startPanel.transform.Find("PlayButton").GetComponent<Button>();
        replayButton = gameoverPanel.transform.Find("ReplayButton").GetComponent<Button>();
        bestScoreToggle = gameoverPanel.transform.Find("BestScoreToggle").gameObject;
        bestScoreText = gameoverPanel.transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();


        replayButton.onClick.AddListener(ReplayOnClick);
        playButton.onClick.AddListener(PlayOnClick);

        startPanel.SetActive(false);
        gameoverPanel.SetActive(false);
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

    public void ShowGameoverScreen(int finalScore, int bestScore)
    {
        currentScoreText.gameObject.SetActive(false);
        gameoverPanel.SetActive(true);
        startPanel.SetActive(false);
        finalScoreText.SetText(finalScore.ToString());
        bestScoreText.SetText("Best score: " + bestScore.ToString());
    }

    public void ToggleBestScoreText(bool isShowing)
    {
        bestScoreToggle.SetActive(isShowing);
    }

    public void EnableCurrentScore()
    {
        currentScoreText.gameObject.SetActive(true);
    }

    public void ShowStartScreen()
    {
        startPanel.SetActive(true);
        gameoverPanel.SetActive(false);
        currentScoreText.gameObject.SetActive(false);
    }

    public void UpdateCurrentScoreUI(int currScore)
    {
        currentScoreText.SetText(currScore.ToString());
    }

    
}
