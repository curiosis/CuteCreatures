using UnityEngine;

public class NaturalResourcesAnimal : FarmAnimalBase
{
    [Header("Natural Resources")]
    [SerializeField]
    private GameObject resourcePrefab;
    [SerializeField]
    private float resourceCooldown = 300f;
    [SerializeField]
    private bool requireTool = true;
    [SerializeField]
    private ToolType requiredTool = ToolType.None;
    [SerializeField]
    private bool autoGenerate = false;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private int maxResources = 5;

    private float resourceTimer = 0f;
    private int currentResources = 0;

    protected override void Start()
    {
        base.Start();
        resourceTimer = resourceCooldown;
    }

    private void Update()
    {
        HandleResourceCooldown();

        if (autoGenerate)
        {
            HandleAutoGeneration();
        }
    }

    private void HandleResourceCooldown()
    {
        if (!autoGenerate && currentResources == 0)
        {
            resourceTimer -= Time.deltaTime;
            if (resourceTimer <= 0)
            {
                currentResources = 1;
                resourceTimer = 0;
            }
        }
    }

    private void HandleAutoGeneration()
    {
        if (currentResources < maxResources)
        {
            resourceTimer -= Time.deltaTime;
            if (resourceTimer <= 0)
            {
                SpawnResource();
                currentResources++;
                resourceTimer = resourceCooldown;
            }
        }
    }
    public override void Interact(PlayerInventory playerInventory)
    {
        if (currentResources > 0 && !autoGenerate)
        {
            if (requireTool)
            {
                Item currentItem = playerInventory.GetCurrentHotBarItem();

                if (currentItem != null && currentItem is Tool tool && tool.ToolType == requiredTool)
                {
                    SpawnResource();
                    currentResources--;
                }
                else
                {
                    Debug.Log($"This animal requires a {requiredTool} to collect its resource.");
                }
            }
            else
            {
                SpawnResource();
                currentResources--;
            }

            resourceTimer = resourceCooldown;
        }
        else if (autoGenerate)
        {
            Debug.Log("Resources are being generated automatically.");
        }
        else
        {
            base.Interact(playerInventory);
            Debug.Log("Resource is not yet ready.");
        }
    }

    private void SpawnResource()
    {
        if (resourcePrefab != null && spawnPoint != null)
        {
            Instantiate(resourcePrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"{resourcePrefab.name} has been spawned at {spawnPoint.position}");
        }
        else
        {
            Debug.LogWarning("Resource prefab or spawn point is not set!");
        }
    }

    public void ResetResources()
    {
        currentResources = 0;
        resourceTimer = resourceCooldown;
    }

    public bool IsResourceReady()
    {
        return currentResources > 0 || autoGenerate;
    }
}