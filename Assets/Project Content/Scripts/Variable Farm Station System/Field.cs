using UnityEngine;

public class Field : InteractableObject
{
    [Header("Field Settings")]
    [SerializeField] 
    private CropBase plantedCrop;
    [SerializeField] 
    private bool isOccupied = false;
    [SerializeField] 
    private float requiredSpace = 1f;

    [Header("References")]
    [SerializeField] private Transform cropSpawnPoint;

    public override void OnInteract()
    {
        if (isOccupied)
        {
            HarvestCrop();
        }
        else
        {
            // PlantCrop();
        }
    }

    /*

    private void PlantCrop()
    {
        if (Player.Instance.Inventory.TryGetSelectedSeed(out SeedItem seed))
        {
            if (CanPlant(seed))
            {
                plantedCrop = Instantiate(seed.GetCropPrefab(), cropSpawnPoint.position, Quaternion.identity).GetComponent<CropBase>();
                plantedCrop.Initialize(this);
                isOccupied = true;
                Debug.Log($"Planted {seed.GetCropName()} on the field.");
                PlayerInventory.Instance.RemoveItem(seed, 1); // Usuniêcie nasion z ekwipunku
            }
            else
            {
                Debug.Log("Cannot plant here. Not enough space or invalid seed.");
            }
        }
        else
        {
            Debug.Log("No seeds selected.");
        }
    }

    */

    private void HarvestCrop()
    {
        if (plantedCrop != null && plantedCrop.Harvest())
        {
            Destroy(plantedCrop.gameObject);
            plantedCrop = null;
            isOccupied = false;
            Debug.Log("Crop harvested successfully.");
        }
        else
        {
            Debug.Log("No harvestable crop on this field.");
        }
    }

    public bool CanPlant(SeedItem seed)
    {
        //return !isOccupied && seed.GetRequiredSpace() <= requiredSpace;
        return false;
    }


    public void ClearField()
    {
        if (plantedCrop != null)
        {
            Destroy(plantedCrop.gameObject);
            plantedCrop = null;
        }

        isOccupied = false;
        Debug.Log("Field cleared.");
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}