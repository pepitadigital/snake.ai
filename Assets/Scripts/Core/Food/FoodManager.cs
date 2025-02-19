using UnityEngine;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private GameObject normalFoodPrefab;
    [SerializeField] private GameObject powerFoodPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float powerFoodChance = 0.2f;
    [SerializeField] private int maxFoodCount = 3;
    [SerializeField] private float spawnInterval = 2f;

    private List<FoodBase> activeFood = new List<FoodBase>();
    private float spawnTimer;

    private void Update()
    {
        // Auto spawn food if needed
        if (activeFood.Count < maxFoodCount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnFood();
            }
        }
    }

    public void SpawnFood()
    {
        Vector2Int position = GetRandomEmptyPosition();
        if (position != Vector2Int.one * -1) // -1,-1 indicates no position found
        {
            GameObject foodPrefab = Random.value < powerFoodChance ? powerFoodPrefab : normalFoodPrefab;
            GameObject foodObj = Instantiate(foodPrefab, Vector3.zero, Quaternion.identity);
            
            FoodBase food = foodObj.GetComponent<FoodBase>();
            food.Initialize(gridSystem, position);
            activeFood.Add(food);
        }
    }

    private Vector2Int GetRandomEmptyPosition()
    {
        Vector2Int gridSize = gridSystem.GetGridSize();
        List<Vector2Int> emptyPositions = new List<Vector2Int>();

        // Find all empty positions
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!gridSystem.IsCellOccupied(pos))
                {
                    emptyPositions.Add(pos);
                }
            }
        }

        // Return random empty position or (-1,-1) if none found
        if (emptyPositions.Count > 0)
        {
            return emptyPositions[Random.Range(0, emptyPositions.Count)];
        }
        return Vector2Int.one * -1;
    }

    public void OnFoodCollected(FoodBase food)
    {
        activeFood.Remove(food);
    }

    public bool IsFoodAtPosition(Vector2Int position)
    {
        return activeFood.Exists(food => food.GetGridPosition() == position);
    }

    public FoodBase GetFoodAtPosition(Vector2Int position)
    {
        return activeFood.Find(food => food.GetGridPosition() == position);
    }
}