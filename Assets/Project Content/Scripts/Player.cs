using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private HealthComponent playerHealth;
    public PlayerInventory Inventory { get; private set; }

    [SerializeField]
    private Image HealthUIBar;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerHealth = GetComponent<HealthComponent>();
        Inventory = GetComponent<PlayerInventory>();
        if (playerHealth == null)
        {
            DebugTool.Instance.CreateLogInfo("HealthComponent is not attached to the Player object!", 3.0f);
        }
    }

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDie += HandlePlayerDeath;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDie -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        DebugTool.Instance.CreateLogInfo("Player has died. Triggering Game Over.", 3.0f);
        // GameManager.Instance.GameOver();
    }

    public void TakeDamage(float damage)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            DebugTool.Instance.CreateLogInfo("Player has taken hit.", 3.0f);
            HealthUIUpdate();
        }
    }

    public void HealthUIUpdate()
    {
        HealthUIBar.fillAmount = playerHealth.Health / playerHealth.MaxHealth;
    }

    public void Heal(float amount)
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(amount);
            HealthUIUpdate();
        }
    }

    public float GetCurrentHealth()
    {
        return playerHealth != null ? playerHealth.Health : 0f;
    }
}