using UnityEngine;

public class PowerFood : FoodBase
{
    [SerializeField] private float multiplierValue = 2f;
    [SerializeField] private float multiplierDuration = 5f;

    private void Awake()
    {
        scoreValue = 30;
        growthAmount = 2;
    }

    public override void OnCollect(SnakeController snake)
    {
        base.OnCollect(snake);
        
        // Adicionar multiplicador temporário
        GameManager.Instance.AddMultiplier(multiplierValue, multiplierDuration);
    }
}