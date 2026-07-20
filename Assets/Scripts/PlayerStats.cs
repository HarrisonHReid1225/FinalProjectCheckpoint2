using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerDepletionRate = 1f;

    [Header("UI References")]
    public Image hungerBarFill;
    public GameManager gameManager;

    void Start()
    {
        currentHunger = maxHunger;
        UpdateUI();
    }

    void Update()
    {
        if (currentHunger > 0)
        {
            currentHunger -= hungerDepletionRate * Time.deltaTime;
            
            if (currentHunger <= 0)
            {
                currentHunger = 0;
                if (gameManager != null)
                {
                    gameManager.GameOver("You died of starvation!");
                }
            }
            UpdateUI();
        }
    }

    public void EatFood(float amount)
    {
        currentHunger = Mathf.Min(currentHunger + amount, maxHunger);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hungerBarFill != null)
        {
            hungerBarFill.fillAmount = currentHunger / maxHunger;
        }
    }
}