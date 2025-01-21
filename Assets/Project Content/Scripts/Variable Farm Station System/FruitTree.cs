using UnityEngine;

public class FruitTree : CropBase
{
    [Header("Fruit Tree Settings")]
    [SerializeField] 
    private float fruitRipeningTime;
    [SerializeField] 
    private int fruitYieldPerCycle;

    private bool isRipe = false;

    private void Update()
    {
        if (!isHarvestable)
        {
            Grow(Time.deltaTime);
        }
        else if (!isRipe)
        {
            HandleFruitRipening(Time.deltaTime);
        }
    }

    private void HandleFruitRipening(float deltaTime)
    {
        growthProgress += deltaTime / fruitRipeningTime;
        if (growthProgress >= 1f)
        {
            OnFruitRipened();
        }
    }

    private void OnFruitRipened()
    {
        isRipe = true;
        Debug.Log($"{cropName}'s fruits are ripe and ready for harvest.");
    }

    public override bool Harvest()
    {
        if (!isRipe)
        {
            Debug.Log($"{cropName} is not ready for harvest.");
            return false;
        }

        Debug.Log($"Harvested {fruitYieldPerCycle} fruits from {cropName}.");
        SpawnHarvestItem();
        isRipe = false;
        growthProgress = 0f;
        return true;
    }
}