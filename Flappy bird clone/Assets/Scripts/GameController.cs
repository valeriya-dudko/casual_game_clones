using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public bool IsPlaying { get; private set; } = false;
    const int queueCapacity = 5;
    int currScore = 0;

    float pipeSpawnCooldown = 2f;
    float pipeSpawnPosX = 7f;
    float pipeSpawnPosYMin = -2f;
    float pipeSpawnPosYMax = 2f;

    public float DifficultyMultiplier { get; private set; } = 1.0f;
    const float difficultyIncrease = 0.02f;

    [SerializeField]
    GameObject pipePrefab;
    Queue<GameObject> pipeQ = new Queue<GameObject>(queueCapacity);

    GameObject scrollersParent;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        AudioController.Instance.PlayMusic("mainLevel");

        UIController.Instance.InitUIObjects();
        scrollersParent = GameObject.Find("Scrollers");
        if (LevelController.Instance.FirstStart)
        {
            UIController.Instance.ShowStartScreen();
            PlayerController.Instance.rb.Sleep();
            foreach (Transform scrolls in scrollersParent.transform)
                scrolls.GetComponent<IScrollable>().StopScrolling();
        }
        else
            StartGame();

    }

    void GeneratePipe(Vector3 position)
    {
        GameObject pipe = GameObject.Instantiate(pipePrefab, position, new Quaternion(), scrollersParent.transform);
        pipeQ.Enqueue(pipe);
    }
    public void RegeneratePipe(Vector3 position)
    {
        GameObject pipe = pipeQ.Dequeue();
        pipe.transform.position = position;
        pipeQ.Enqueue(pipe);
    }
    public void DeQPipe()
    {
        pipeQ.Dequeue();
    }
    IEnumerator SpawnPipe()
    {
        if (IsPlaying)
        {
            if (pipeQ.Count < queueCapacity)
                GeneratePipe(new Vector2(pipeSpawnPosX, Random.Range(pipeSpawnPosYMin, pipeSpawnPosYMax)));
            else
                RegeneratePipe(new Vector2(pipeSpawnPosX, Random.Range(pipeSpawnPosYMin, pipeSpawnPosYMax)));
            yield return new WaitForSeconds(pipeSpawnCooldown / DifficultyMultiplier);
            StartCoroutine(SpawnPipe());
        }
    }

    public void IncreaseScore()
    {
        currScore++;
        IncreaseDifficulty();
        UIController.Instance.UpdateCurrentScoreUI(currScore);
    }

    public void IncreaseDifficulty()
    {
        DifficultyMultiplier += difficultyIncrease;
    }

    public void EndGame()
    {
        IsPlaying = false;

        AudioController.Instance.PlayMusic("");
        AudioController.Instance.PlaySFX("gameover");

        GameObject ScrollParent = GameObject.Find("Scrollers");
        foreach (Transform scrolls in ScrollParent.transform)
            scrolls.GetComponent<IScrollable>().StopScrolling();
        int bestScore;
        bestScore = PlayerPrefs.GetInt("bestScoreFBC");
        Debug.Log(bestScore);
        if (currScore > bestScore)
        {
            bestScore = currScore;
            PlayerPrefs.SetInt("bestScoreFBC", bestScore);
            UIController.Instance.ToggleBestScoreText(true);
        }
        else
            UIController.Instance.ToggleBestScoreText(false);

        UIController.Instance.ShowGameoverScreen(currScore, bestScore);
    }

    public void StartGame()
    {
        UIController.Instance.EnableCurrentScore();
        LevelController.Instance.FirstStart = false;
        IsPlaying = true;
        StartCoroutine(SpawnPipe());
        foreach (Transform scrolls in scrollersParent.transform)
            scrolls.GetComponent<IScrollable>().StartScrolling();
        PlayerController.Instance.rb.WakeUp();

        
    }
}
