using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(20, 30);
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private bool showDebugGrid = true;

    private bool[,] occupiedCells;
    
    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        occupiedCells = new bool[gridSize.x, gridSize.y];
    }

    private void OnDrawGizmos()
    {
        if (!showDebugGrid) return;

        // Draw grid cells
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 position = GridToWorldPosition(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, Vector3.one * cellSize);
            }
        }

        // Draw grid bounds
        Gizmos.color = Color.blue;
        Vector3 boundsCenter = GridToWorldPosition(new Vector2Int(gridSize.x / 2, gridSize.y / 2));
        Vector3 boundsSize = new Vector3(gridSize.x * cellSize, gridSize.y * cellSize, 0.1f);
        Gizmos.DrawWireCube(boundsCenter, boundsSize);
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        float x = (gridPos.x - gridSize.x / 2f + 0.5f) * cellSize;
        float y = (gridPos.y - gridSize.y / 2f + 0.5f) * cellSize;
        return new Vector3(x, y, 0);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cellSize + gridSize.x / 2f - 0.5f);
        int y = Mathf.RoundToInt(worldPos.y / cellSize + gridSize.y / 2f - 0.5f);
        return new Vector2Int(x, y);
    }

    public bool IsValidPosition(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gridSize.x &&
               gridPos.y >= 0 && gridPos.y < gridSize.y &&
               !occupiedCells[gridPos.x, gridPos.y];
    }

    public void SetCellOccupied(Vector2Int gridPos, bool occupied)
    {
        if (IsWithinBounds(gridPos))
        {
            occupiedCells[gridPos.x, gridPos.y] = occupied;
        }
    }

    public bool IsCellOccupied(Vector2Int gridPos)
    {
        return IsWithinBounds(gridPos) && occupiedCells[gridPos.x, gridPos.y];
    }

    private bool IsWithinBounds(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gridSize.x &&
               gridPos.y >= 0 && gridPos.y < gridSize.y;
    }

    public Vector2Int GetGridSize()
    {
        return gridSize;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
}
