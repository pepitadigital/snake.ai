using UnityEngine;
using System.Collections.Generic;

public enum GameState
{
    Menu,
    PowerSelection,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private SnakeController snake;
    [SerializeField] private GridSystem grid;
    [SerializeField] private FoodManager foodManager;
    
    [Header("Score Settings")]
    [SerializeField] private int baseScorePerFood = 10;
    
    private GameState currentState;
    private int currentScore;
    private float currentMultiplier = 1f;
    private List<MultiplierEffect> activeMultipliers = new List<MultiplierEffect>();

    public GameState CurrentState => currentState;
    public int CurrentScore => currentScore;
    public float CurrentMultiplier => CalculateTotalMultiplier();

    // Events
    public event System.Action<GameState> OnGameStateChanged;
    public event System.Action<int> OnScoreChanged;
    public event System.Action<float> OnMultiplierChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeGame();
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            UpdateMultipliers();
        }
    }

    private void InitializeGame()
    {
        SetGameState(GameState.Menu);
        ResetGame();
    }

    private void ResetGame()
    {
        currentScore = 0;
        currentMultiplier = 1f;
        activeMultipliers.Clear();
        OnScoreChanged?.Invoke(currentScore);
        OnMultiplierChanged?.Invoke(currentMultiplier);
    }

    public void StartPowerSelection()
    {
        ResetGame();
        SetGameState(GameState.PowerSelection);
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
        snake.Initialize();
        foodManager.SpawnFood();
    }

    public void ReturnToMainMenu()
    {
        SetGameState(GameState.Menu);
        ResetGame();
    }

    private void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
    }

    private void HandleCollision()
    {
        if (currentState != GameState.Playing) return;
        
        SetGameState(GameState.GameOver);
        SaveHighScore();
    }

    private void HandleFoodCollected(FoodBase food)
    {
        if (currentState != GameState.Playing) return;

        int scoreToAdd = Mathf.RoundToInt(food.GetScoreValue() * CurrentMultiplier);
        AddScore(scoreToAdd);
    }

    public void AddMultiplier(float value, float duration)
    {
        activeMultipliers.Add(new MultiplierEffect(value, duration));
        OnMultiplierChanged?.Invoke(CalculateTotalMultiplier());
    }

    private void UpdateMultipliers()
    {
        bool multiplierChanged = false;
        for (int i = activeMultipliers.Count - 1; i >= 0; i--)
        {
            activeMultipliers[i].RemainingDuration -= Time.deltaTime;
            if (activeMultipliers[i].RemainingDuration <= 0)
            {
                activeMultipliers.RemoveAt(i);
                multiplierChanged = true;
            }
        }

        if (multiplierChanged)
        {
            OnMultiplierChanged?.Invoke(CalculateTotalMultiplier());
        }
    }

    private float CalculateTotalMultiplier()
    {
        float total = 1f;
        foreach (var multiplier in activeMultipliers)
        {
            total *= multiplier.Value;
        }
        return total;
    }

    private void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);
    }

    private void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}

public class MultiplierEffect
{
    public float Value { get; private set; }
    public float RemainingDuration { get; set; }

    public MultiplierEffect(float value, float duration)
    {
        Value = value;
        RemainingDuration = duration;
    }
}