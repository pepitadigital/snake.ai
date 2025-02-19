using UnityEngine;

public class NormalFood : FoodBase
{
    private void Awake()
    {
        scoreValue = 10;
        growthAmount = 1;
    }
}