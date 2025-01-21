using UnityEngine;

public abstract class CropBase : MonoBehaviour
{
    [Header("Crop Properties")]
    [SerializeField] 
    protected string cropName;
    [SerializeField] 
    protected float growthTime;
    [SerializeField] 
    protected int harvestYield;
    [SerializeField] 
    protected Item harvestItem;

    protected float growthProgress = 0f;
    protected bool isHarvestable = false;

    public virtual void Plant()
    {
        Debug.Log($"{cropName} has been planted.");
        growthProgress = 0f;
        isHarvestable = false;
    }

    protected virtual void Grow(float deltaTime)
    {
        if (isHarvestable)
            return;

        growthProgress += deltaTime / growthTime;
        growthProgress = Mathf.Clamp01(growthProgress);

        if (growthProgress >= 1f)
        {
            OnGrowthComplete();
        }
    }

    protected virtual void OnGrowthComplete()
    {
        Debug.Log($"{cropName} has fully grown.");
        isHarvestable = true;
    }

    public virtual bool Harvest()
    {
        if (!isHarvestable)
        {
            Debug.Log($"{cropName} is not ready for harvest.");
            return false;
        }

        Debug.Log($"Harvested {harvestYield} of {cropName}.");
        SpawnHarvestItem();
        return true;
    }

    protected virtual void SpawnHarvestItem()
    {
        for (int i = 0; i < harvestYield; i++)
        {
            Instantiate(harvestItem, transform.position, Quaternion.identity);
        }
    }

    public bool IsHarvestable()
    {
        return isHarvestable;
    }

    public float GetGrowthProgress()
    {
        return growthProgress;
    }
}