using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthBoostText;
    [SerializeField] private TextMeshProUGUI speedBoostText;
    [SerializeField] private Image healthBoostIcon;
    [SerializeField] private Image speedBoostIcon;
    [SerializeField] private GameObject progressionPanel;
    
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 1.0f;
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private PlayerProgression playerProgression;
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    
    void Start()
    {
        // Get references
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        originalScale = transform.localScale;
        
        // Find player progression component
        if (GameStateManager.Instance != null)
        {
            var playerController = GameStateManager.Instance.GetComponentInChildren<PlayerController>();
            if (playerController != null && playerController.Character != null)
            {
                playerProgression = playerController.Character.GetComponent<PlayerProgression>();
            }
        }
        
        // Subscribe to progression events
        if (playerProgression != null)
        {
            playerProgression.OnHealthBoostApplied.AddListener(OnHealthBoostApplied);
            playerProgression.OnSpeedBoostApplied.AddListener(OnSpeedBoostApplied);
        }
        
        // Initially hide the panel
        if (progressionPanel != null)
        {
            progressionPanel.SetActive(false);
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        if (playerProgression != null)
        {
            playerProgression.OnHealthBoostApplied.RemoveListener(OnHealthBoostApplied);
            playerProgression.OnSpeedBoostApplied.RemoveListener(OnSpeedBoostApplied);
        }
    }
    
    private void OnHealthBoostApplied(float multiplier)
    {
        UpdateHealthDisplay(multiplier);
        ShowProgressionNotification("Health Boost!", multiplier);
    }
    
    private void OnSpeedBoostApplied(float multiplier)
    {
        UpdateSpeedDisplay(multiplier);
        ShowProgressionNotification("Speed Boost!", multiplier);
    }
    
    private void UpdateHealthDisplay(float multiplier)
    {
        if (healthBoostText != null)
        {
            int percentage = Mathf.RoundToInt((multiplier - 1.0f) * 100);
            healthBoostText.text = $"+{percentage}%";
        }
        
        if (healthBoostIcon != null)
        {
            // Change color based on boost level
            float intensity = Mathf.Clamp01((multiplier - 1.0f) / 2.0f);
            healthBoostIcon.color = Color.Lerp(Color.green, Color.red, intensity);
        }
    }
    
    private void UpdateSpeedDisplay(float multiplier)
    {
        if (speedBoostText != null)
        {
            int percentage = Mathf.RoundToInt((multiplier - 1.0f) * 100);
            speedBoostText.text = $"+{percentage}%";
        }
        
        if (speedBoostIcon != null)
        {
            // Change color based on boost level
            float intensity = Mathf.Clamp01((multiplier - 1.0f) / 1.0f);
            speedBoostIcon.color = Color.Lerp(Color.blue, Color.cyan, intensity);
        }
    }
    
    private void ShowProgressionNotification(string message, float multiplier)
    {
        if (progressionPanel != null)
        {
            progressionPanel.SetActive(true);
            
            // Animate the notification
            StartCoroutine(AnimateNotification());
        }
    }
    
    private System.Collections.IEnumerator AnimateNotification()
    {
        float elapsedTime = 0f;
        Vector3 startScale = originalScale * 0.5f;
        Vector3 endScale = originalScale;
        
        // Scale up animation
        while (elapsedTime < animationDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (animationDuration * 0.5f);
            float curveValue = scaleCurve.Evaluate(progress);
            
            transform.localScale = Vector3.Lerp(startScale, endScale, curveValue);
            canvasGroup.alpha = curveValue;
            
            yield return null;
        }
        
        // Hold for a moment
        yield return new WaitForSeconds(1.0f);
        
        // Scale down animation
        elapsedTime = 0f;
        while (elapsedTime < animationDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (animationDuration * 0.5f);
            float curveValue = 1.0f - scaleCurve.Evaluate(progress);
            
            transform.localScale = Vector3.Lerp(startScale, endScale, curveValue);
            canvasGroup.alpha = curveValue;
            
            yield return null;
        }
        
        // Hide the panel
        if (progressionPanel != null)
        {
            progressionPanel.SetActive(false);
        }
        
        transform.localScale = originalScale;
        canvasGroup.alpha = 1.0f;
    }
    
    public void UpdateDisplay()
    {
        if (playerProgression != null)
        {
            UpdateHealthDisplay(playerProgression.GetCurrentHealthMultiplier());
            UpdateSpeedDisplay(playerProgression.GetCurrentSpeedMultiplier());
        }
    }
}
