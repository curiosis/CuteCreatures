using System.Collections.Generic;
using UnityEngine;

public class HarvestStation : MonoBehaviour
{
    [Header("Harvest Station Settings")]
    [SerializeField] 
    private List<Field> fields;
    [SerializeField] 
    private Vector2Int gridSize = new Vector2Int(3, 3);
    [SerializeField] 
    private GameObject fieldPrefab;
    [SerializeField] 
    private Transform fieldParent;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        fields = new List<Field>();

        for (float x = 0; x < 1.5 * gridSize.x; x+=1.5f)
        {
            for (float y = 0; y < 1.5 * gridSize.y; y += 1.5f)
            {
                Vector3 position = new Vector3(x, 0, y);
                Field fieldInstance = Instantiate(fieldPrefab, position, Quaternion.identity, fieldParent).GetComponent<Field>();
                fields.Add(fieldInstance);
            }
        }
    }

    public bool TryPlantCrop(SeedItem seed, Vector3 position)
    {
        Field targetField = GetFieldAtPosition(position);

        if (targetField != null && !targetField.IsOccupied())
        {
            targetField.OnInteract();
            return true;
        }

        Debug.Log("Cannot plant crop. Field is occupied or invalid.");
        return false;
    }

    private Field GetFieldAtPosition(Vector3 position)
    {
        foreach (Field field in fields)
        {
            if (Vector3.Distance(field.transform.position, position) < 0.1f)
            {
                return field;
            }
        }

        return null;
    }

    public bool CanPlant(SeedItem seed)
    {
        foreach (Field field in fields)
        {
            if (!field.IsOccupied() && field.CanPlant(seed))
            {
                return true;
            }
        }

        return false;
    }

    public int GetAvailableFieldCount()
    {
        int availableCount = 0;

        foreach (Field field in fields)
        {
            if (!field.IsOccupied())
            {
                availableCount++;
            }
        }

        return availableCount;
    }

    public void HarvestAll()
    {
        foreach (Field field in fields)
        {
            if (field.IsOccupied())
            {
                field.OnInteract();
            }
        }
    }
}