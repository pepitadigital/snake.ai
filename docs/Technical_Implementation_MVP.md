# Snake.AI - Implementação Técnica MVP

## Estrutura de Arquivos
```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── SnakeController.cs
│   │   ├── GridSystem.cs
│   │   ├── FoodManager.cs
│   │   └── MovementSystem.cs
│   ├── Managers/
│   │   ├── GameManager.cs
│   │   ├── ScoreManager.cs
│   │   └── PowerUpManager.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── ScoreUI.cs
│   │   └── GameOverUI.cs
│   └── ScriptableObjects/
│       ├── PowerUpConfig.cs
│       └── GameConfig.cs
├── Prefabs/
│   ├── SnakeHead.prefab
│   ├── SnakeSegment.prefab
│   ├── Food.prefab
│   └── UI/
└── Scenes/
    ├── MainMenu.unity
    └── Game.unity
```

## Classes Core

### SnakeController
```csharp
public class SnakeController : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private float moveInterval = 0.1f;
    
    private List<Transform> segments = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int gridPosition;
    private float moveTimer;
    private bool canChangeDirection = true;
    
    public event System.Action OnCollision;
    public event System.Action<Vector2Int> OnMove;
    
    private void Update()
    {
        HandleMovement();
        HandleInput();
    }
    
    private void HandleMovement()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            Move();
        }
    }
    
    private void Move()
    {
        // Implementar movimento em grid
        // Atualizar posições dos segmentos
        // Verificar colisões
    }
    
    public void AddSegment()
    {
        // Instanciar novo segmento
        // Adicionar à lista
    }
}
```

### GridSystem
```csharp
public class GridSystem : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(20, 30);
    [SerializeField] private float cellSize = 1f;
    
    private bool[,] occupiedCells;
    
    private void Awake()
    {
        occupiedCells = new bool[gridSize.x, gridSize.y];
    }
    
    public Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0);
    }
    
    public Vector2Int WorldToGridPosition(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / cellSize),
            Mathf.RoundToInt(worldPos.y / cellSize)
        );
    }
    
    public bool IsValidPosition(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gridSize.x &&
               gridPos.y >= 0 && gridPos.y < gridSize.y &&
               !occupiedCells[gridPos.x, gridPos.y];
    }
}
```

### FoodManager
```csharp
public class FoodManager : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject powerFoodPrefab;
    
    [Range(0, 1)]
    [SerializeField] private float powerFoodChance = 0.2f;
    
    private List<Food> activeFood = new List<Food>();
    
    public void SpawnFood()
    {
        Vector2Int position = GetRandomEmptyPosition();
        GameObject foodObj = Instantiate(
            Random.value < powerFoodChance ? powerFoodPrefab : foodPrefab,
            gridSystem.GridToWorldPosition(position),
            Quaternion.identity
        );
        
        activeFood.Add(foodObj.GetComponent<Food>());
    }
    
    private Vector2Int GetRandomEmptyPosition()
    {
        // Implementar lógica para encontrar posição vazia
    }
}
```

## Managers

### GameManager
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private SnakeController snake;
    [SerializeField] private GridSystem grid;
    [SerializeField] private FoodManager foodManager;
    [SerializeField] private UIManager uiManager;
    
    private GameState currentState;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        currentState = GameState.Playing;
        snake.Initialize();
        foodManager.SpawnFood();
    }
    
    public void GameOver()
    {
        currentState = GameState.GameOver;
        uiManager.ShowGameOver();
    }
}
```

## Scriptable Objects

### GameConfig
```csharp
[CreateAssetMenu(fileName = "GameConfig", menuName = "Snake/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Grid Settings")]
    public Vector2Int gridSize = new Vector2Int(20, 30);
    public float cellSize = 1f;
    
    [Header("Snake Settings")]
    public float initialMoveInterval = 0.1f;
    public int initialLength = 4;
    
    [Header("Food Settings")]
    public float powerFoodChance = 0.2f;
    public int normalFoodScore = 10;
    public int powerFoodScore = 30;
}
```

## Plano de Implementação

1. Core Mechanics
   - Implementar GridSystem
   - Criar movimento básico da cobra
   - Sistema de colisão
   - Spawn de comida

2. Managers e Estados
   - GameManager para controle de fluxo
   - Sistema de pontuação
   - Controle de estados (menu, jogo, game over)

3. UI e Feedback
   - Score display
   - Tela de game over
   - Feedback visual de coleta

4. Mobile Input
   - Sistema de input por quadrantes
   - Tratamento de toques
   - Feedback tátil

5. Polish
   - Animações básicas
   - Efeitos sonoros
   - Otimizações de performance