using UnityEngine;
using System.Collections.Generic;

public enum GameState
{
    Menu,
    Playing,
    Paused,
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        UpdateMultipliers();
    }

    private void InitializeGame()
    {
        currentState = GameState.Menu;
        currentScore = 0;
        currentMultiplier = 1f;
        activeMultipliers.Clear();

        // Subscribe to events
        snake.OnCollision += HandleCollision;
        snake.OnFoodCollected += HandleFoodCollected;
    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        currentScore = 0;
        currentMultiplier = 1f;
        activeMultipliers.Clear();

        snake.Initialize();
        foodManager.SpawnFood();
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
            currentState = GameState.Paused;
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
            currentState = GameState.Playing;
    }

    private void HandleCollision()
    {
        if (currentState != GameState.Playing) return;
        
        currentState = GameState.GameOver;
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
    }

    private void UpdateMultipliers()
    {
        for (int i = activeMultipliers.Count - 1; i >= 0; i--)
        {
            activeMultipliers[i].RemainingDuration -= Time.deltaTime;
            if (activeMultipliers[i].RemainingDuration <= 0)
            {
                activeMultipliers.RemoveAt(i);
            }
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