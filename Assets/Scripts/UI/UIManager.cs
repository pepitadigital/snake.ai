using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject powerSelectionPanel;

    private void Start()
    {
        // Subscribe to game events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        }

        // Initial UI setup
        ShowMainMenu();
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                ShowMainMenu();
                break;
            case GameState.PowerSelection:
                ShowPowerSelection();
                break;
            case GameState.Playing:
                ShowGameplay();
                break;
            case GameState.GameOver:
                ShowGameOver();
                break;
        }
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        powerSelectionPanel.SetActive(false);
    }

    private void ShowPowerSelection()
    {
        mainMenuPanel.SetActive(false);
        powerSelectionPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void ShowGameplay()
    {
        mainMenuPanel.SetActive(false);
        powerSelectionPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    private void ShowGameOver()
    {
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    // UI Button Callbacks
    public void OnStartGameClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartPowerSelection();
        }
    }

    public void OnPowerSelected()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    public void OnRestartClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartPowerSelection();
        }
    }

    public void OnMainMenuClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMainMenu();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }
}