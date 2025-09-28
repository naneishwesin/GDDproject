using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProgression : MonoBehaviour
{
    [Header("Progression Settings")]
    [Tooltip("Health boost per round (percentage of starting health)")]
    public float healthBoostPerRound = 0.1f; // 10% increase per round
    
    [Tooltip("Speed boost per round (percentage of base speed)")]
    public float speedBoostPerRound = 0.05f; // 5% increase per round
    
    [Tooltip("Maximum health boost multiplier (e.g., 2.0 = 200% max health)")]
    public float maxHealthMultiplier = 3.0f;
    
    [Tooltip("Maximum speed boost multiplier (e.g., 2.0 = 200% max speed)")]
    public float maxSpeedMultiplier = 2.0f;
    
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    
    [Header("Events")]
    public UnityEvent<float> OnHealthBoostApplied;
    public UnityEvent<float> OnSpeedBoostApplied;
    
    private int baseStartingHealth;
    private float baseSpeed;
    private int lastAppliedRound = -1;
    
    void Start()
    {
        // Store base values
        if (playerHealth != null)
        {
            baseStartingHealth = playerHealth.startingHealth;
        }
        
        if (playerMovement != null)
        {
            baseSpeed = playerMovement.speed;
        }
        
        // Apply initial progression if game has already started
        if (GameStateManager.Instance != null)
        {
            ApplyProgressionForRound(GameStateManager.Instance.CurrentRound);
        }
    }
    
    void Update()
    {
        // Check if we need to apply progression for a new round
        if (GameStateManager.Instance != null)
        {
            int currentRound = GameStateManager.Instance.CurrentRound;
            if (currentRound > lastAppliedRound)
            {
                ApplyProgressionForRound(currentRound);
            }
        }
    }
    
    public void ApplyProgressionForRound(int roundNumber)
    {
        if (roundNumber < 0) return;
        
        lastAppliedRound = roundNumber;
        
        // Calculate boost multipliers
        float healthMultiplier = 1.0f + (healthBoostPerRound * roundNumber);
        float speedMultiplier = 1.0f + (speedBoostPerRound * roundNumber);
        
        // Clamp to maximum values
        healthMultiplier = Mathf.Min(healthMultiplier, maxHealthMultiplier);
        speedMultiplier = Mathf.Min(speedMultiplier, maxSpeedMultiplier);
        
        // Apply health boost
        if (playerHealth != null)
        {
            int newMaxHealth = Mathf.RoundToInt(baseStartingHealth * healthMultiplier);
            
            // Calculate how much health to add (heal the player)
            int healthToAdd = newMaxHealth - playerHealth.maxHealth;
            
            // Update max health
            playerHealth.SetMaxHealth(newMaxHealth);
            
            // Heal the player by the difference
            if (healthToAdd > 0)
            {
                playerHealth.Heal(healthToAdd);
            }
            
            // Fire progression event
            OnHealthBoostApplied.Invoke(healthMultiplier);
            
            Debug.Log($"Round {roundNumber + 1}: Health boosted to {newMaxHealth} (x{healthMultiplier:F2})");
        }
        
        // Apply speed boost
        if (playerMovement != null)
        {
            float newSpeed = baseSpeed * speedMultiplier;
            playerMovement.speed = newSpeed;
            
            // Fire progression event
            OnSpeedBoostApplied.Invoke(speedMultiplier);
            
            Debug.Log($"Round {roundNumber + 1}: Speed boosted to {newSpeed:F1} (x{speedMultiplier:F2})");
        }
    }
    
    public float GetCurrentHealthMultiplier()
    {
        if (GameStateManager.Instance != null)
        {
            int currentRound = GameStateManager.Instance.CurrentRound;
            float multiplier = 1.0f + (healthBoostPerRound * currentRound);
            return Mathf.Min(multiplier, maxHealthMultiplier);
        }
        return 1.0f;
    }
    
    public float GetCurrentSpeedMultiplier()
    {
        if (GameStateManager.Instance != null)
        {
            int currentRound = GameStateManager.Instance.CurrentRound;
            float multiplier = 1.0f + (speedBoostPerRound * currentRound);
            return Mathf.Min(multiplier, maxSpeedMultiplier);
        }
        return 1.0f;
    }
    
    public int GetCurrentMaxHealth()
    {
        return Mathf.RoundToInt(baseStartingHealth * GetCurrentHealthMultiplier());
    }
    
    public int GetBaseStartingHealth()
    {
        return baseStartingHealth;
    }
    
    public float GetBaseSpeed()
    {
        return baseSpeed;
    }
    
    public float GetCurrentMaxSpeed()
    {
        return baseSpeed * GetCurrentSpeedMultiplier();
    }
}
