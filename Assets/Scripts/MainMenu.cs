using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerController;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button gameOverRestartButton;
    [SerializeField] Button gameOverQuitButton;
    [SerializeField] Button pauseQuitButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    private bool gameStarted = false;
    private bool paused = false;

    private void OnEnable()
    {
        PlayerController.GameOver += ShowGameOverScreen;
        SpawnManager.ScoreCountEnded += PrintScore;
    }
    private void OnDisable()
    {
        PlayerController.GameOver -= ShowGameOverScreen;
        SpawnManager.ScoreCountEnded -= PrintScore;
    }

    void Start()
    {
        PrintWave(0);
        Time.timeScale = 0.0f;
        mainMenu.SetActive(true);
        startButton.onClick.AddListener(StartTheGame);
        gameOverRestartButton.onClick.AddListener(RestartTheGame);
        quitButton.onClick.AddListener(QuitTheGame);
        gameOverQuitButton.onClick.AddListener(QuitTheGame);   
        pauseQuitButton.onClick.AddListener(QuitTheGame);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused && gameStarted)
            {
                paused = true;
                gameStarted = false;
                gameStarted = false;
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
            }
            else if (paused && !gameStarted)
            {
                paused = false;
                gameStarted = true;
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
            }
        }
    }

    private void StartTheGame()
    {
        waveText.gameObject.SetActive(true);
        gameStarted = true;
        Time.timeScale = 1.0f;
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitTheGame()
    {
        gameStarted = false;
        Application.Quit();
    }

    private void ShowGameOverScreen()
    {
        gameStarted = false;
        waveText.gameObject.SetActive(false);
        Time.timeScale = 0.0f;
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PrintWave(int wave)
    {
        waveText.text = "Wave: " + wave.ToString();
    }

    private void PrintScore(int score)
    {
        if (score > 1 || score == 0)
        {
            scoreText.text = score.ToString() + " waves";
        }
        else
        {
            scoreText.text = score.ToString() + " wave";
        }
    }
}
