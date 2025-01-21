using UnityEngine;

public class Crop : CropBase
{
    [Header("Crop-Specific Settings")]
    [SerializeField] 
    private float witherTime;
    private bool isWithered = false;

    private float witherProgress = 0f;

    private void Update()
    {
        if (!isHarvestable && !isWithered)
        {
            Grow(Time.deltaTime);
        }
        else if (isHarvestable && witherTime > 0)
        {
            HandleWithering(Time.deltaTime);
        }
    }

    private void HandleWithering(float deltaTime)
    {
        if (isWithered)
            return;

        witherProgress += deltaTime / witherTime;
        if (witherProgress >= 1f)
        {
            OnWither();
        }
    }

    private void OnWither()
    {
        isWithered = true;
        Debug.Log($"{cropName} has withered.");
    }

    public override bool Harvest()
    {
        if (isWithered)
        {
            Debug.Log($"{cropName} is withered and cannot be harvested.");
            return false;
        }

        return base.Harvest();
    }
}