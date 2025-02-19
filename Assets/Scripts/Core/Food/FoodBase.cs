using UnityEngine;

public abstract class FoodBase : MonoBehaviour
{
    [SerializeField] protected int scoreValue;
    [SerializeField] protected int growthAmount = 1;
    [SerializeField] protected ParticleSystem collectEffect;

    protected Vector2Int gridPosition;
    protected GridSystem gridSystem;

    public virtual void Initialize(GridSystem grid, Vector2Int position)
    {
        gridSystem = grid;
        gridPosition = position;
        transform.position = gridSystem.GridToWorldPosition(position);
        gridSystem.SetCellOccupied(position, true);
    }

    public virtual void OnCollect(SnakeController snake)
    {
        // Spawn effect if available
        if (collectEffect != null)
        {
            var effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration);
        }

        // Growth
        for (int i = 0; i < growthAmount; i++)
        {
            snake.AddSegment();
        }

        // Clear grid position
        gridSystem.SetCellOccupied(gridPosition, false);

        // Destroy food
        Destroy(gameObject);
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }
}