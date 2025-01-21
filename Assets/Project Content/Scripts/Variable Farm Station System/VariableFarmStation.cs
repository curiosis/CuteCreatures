using UnityEngine;

public class VariableFarmStation : InteractableObject
{
    [Header("Farm Station Properties")]
    [SerializeField] 
    private int cost;
    [SerializeField] 
    private ESkillType requiredSkill;
    [SerializeField] 
    private int requiredSkillLevel;
    [SerializeField] 
    private GameObject harvestStationPrefab;
    [SerializeField] 
    private GameObject animalStationPrefab;

    private bool isPurchased = false;

    public override void OnInteract()
    {
        if (isPurchased)
        {
            Debug.Log("Station is already purchased and cannot be interacted with further.");
            return;
        }

        if (CanPurchase())
        {
            OpenPurchaseMenu();
        }
        else
        {
            Debug.Log("You do not meet the requirements to purchase this station.");
        }
    }

    private bool CanPurchase()
    {
        Player player = Player.Instance;
        // bool hasEnoughMoney = player.Currency >= cost;
        // bool hasRequiredSkill = player.GetSkillLevel(requiredSkill) >= requiredSkillLevel;

        //return hasEnoughMoney && hasRequiredSkill;

        return false;
    }

    private void OpenPurchaseMenu()
    {
        // Placeholder for UI menu to select station type
        Debug.Log("Opening purchase menu for Variable Farm Station...");
        // Assume for now a Harvest Station is selected by default
        PurchaseStation(isHarvest: true);
    }

    private void PurchaseStation(bool isHarvest)
    {
        Player player = Player.Instance;

        if (!CanPurchase())
        {
            Debug.Log("Purchase failed due to insufficient requirements.");
            return;
        }

        //player.Currency -= cost;

        GameObject stationPrefab = isHarvest ? harvestStationPrefab : animalStationPrefab;
        Instantiate(stationPrefab, transform.position, transform.rotation);

        isPurchased = true;
        gameObject.SetActive(false);
    }
}