using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private FoodManager foodManager;
    [SerializeField] private GameObject segmentPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float moveInterval = 0.1f;
    [SerializeField] private int initialLength = 4;

    private List<Transform> segments = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int gridPosition;
    private float moveTimer;
    private bool canChangeDirection = true;

    public event System.Action OnCollision;
    public event System.Action<Vector2Int> OnMove;
    public event System.Action<FoodBase> OnFoodCollected;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // Clear existing segments
        foreach (var segment in segments)
        {
            if (segment != null)
                Destroy(segment.gameObject);
        }
        segments.Clear();

        // Create initial snake
        gridPosition = new Vector2Int(gridSystem.GetGridSize().x / 4, gridSystem.GetGridSize().y / 2);
        transform.position = gridSystem.GridToWorldPosition(gridPosition);
        segments.Add(transform);

        // Add initial segments
        for (int i = 1; i < initialLength; i++)
        {
            AddSegment();
        }

        direction = Vector2Int.right;
        canChangeDirection = true;
        moveTimer = 0f;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;
        
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (!canChangeDirection) return;

        Vector2Int newDirection = direction;

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2Int.down)
            newDirection = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2Int.up)
            newDirection = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2Int.right)
            newDirection = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2Int.left)
            newDirection = Vector2Int.right;

        if (newDirection != direction)
        {
            direction = newDirection;
            canChangeDirection = false;
        }
    }

    private void HandleMovement()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            Move();
            canChangeDirection = true;
        }
    }

    private void Move()
    {
        // Calculate new position
        Vector2Int newPosition = gridPosition + direction;

        // Check for wall collisions
        if (!gridSystem.IsWithinBounds(newPosition))
        {
            OnCollision?.Invoke();
            return;
        }

        // Check for self collision (except when moving away from a position)
        if (gridSystem.IsCellOccupied(newPosition) && !IsTailPosition(newPosition))
        {
            OnCollision?.Invoke();
            return;
        }

        // Check for food
        if (foodManager.IsFoodAtPosition(newPosition))
        {
            FoodBase food = foodManager.GetFoodAtPosition(newPosition);
            CollectFood(food);
        }

        // Update positions
        Vector2Int oldTailPosition = gridSystem.WorldToGridPosition(segments[segments.Count - 1].position);
        gridSystem.SetCellOccupied(oldTailPosition, false); // Clear old tail position

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
            Vector2Int segmentGridPos = gridSystem.WorldToGridPosition(segments[i].position);
            gridSystem.SetCellOccupied(segmentGridPos, true);
        }

        // Move head
        gridPosition = newPosition;
        transform.position = gridSystem.GridToWorldPosition(gridPosition);
        gridSystem.SetCellOccupied(gridPosition, true);

        OnMove?.Invoke(gridPosition);
    }

    private bool IsTailPosition(Vector2Int position)
    {
        if (segments.Count <= 1) return false;
        Vector2Int tailPosition = gridSystem.WorldToGridPosition(segments[segments.Count - 1].position);
        return position == tailPosition;
    }

    private void CollectFood(FoodBase food)
    {
        OnFoodCollected?.Invoke(food);
        food.OnCollect(this);
        foodManager.OnFoodCollected(food);
    }

    public void AddSegment()
    {
        Vector3 newPosition = segments.Count > 0 
            ? segments[segments.Count - 1].position 
            : transform.position;

        GameObject newSegment = Instantiate(segmentPrefab, newPosition, Quaternion.identity);
        segments.Add(newSegment.transform);
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public List<Transform> GetSegments()
    {
        return segments;
    }
}