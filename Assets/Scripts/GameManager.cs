using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 600f;
    public TextMeshProUGUI timerText;
    private bool gameEnded = false;

    [Header("Win Condition Requirements")]
    public Inventory playerInventory;
    public ItemData oreItem;
    public ItemData woodItem;
    public int requiredOre = 5;
    public int requiredWood = 5;

    [Header("Objective & Build Prompt UI")]
    public TextMeshProUGUI objectiveText;
    public GameObject buildPromptUI;

    [Header("Win / Lose UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI loseReasonText;

    void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GetComponent<Inventory>();
        }
    }

    void Update()
    {
        if (gameEnded) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            GameOver("Time ran out!");
            return;
        }

        CheckBuildRequirements();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckBuildRequirements()
    {
        if (playerInventory == null) return;

        int currentOre = playerInventory.GetItemCount(oreItem);
        int currentWood = playerInventory.GetItemCount(woodItem);

        bool hasAllMaterials = (currentOre >= requiredOre && currentWood >= requiredWood);

        if (objectiveText != null)
        {
            if (hasAllMaterials)
            {
                objectiveText.text = "Goal: Ready to build!";
            }
            else
            {
                objectiveText.text = $"Goal: Collect Materials\nOre: {currentOre}/{requiredOre}\nWood: {currentWood}/{requiredWood}";
            }
        }

        if (buildPromptUI != null)
        {
            buildPromptUI.SetActive(hasAllMaterials);
        }

        if (hasAllMaterials && Input.GetKeyDown(KeyCode.B))
        {
            BuildHouse();
        }
    }

    void BuildHouse()
    {
        playerInventory.RemoveItem(oreItem, requiredOre);
        playerInventory.RemoveItem(woodItem, requiredWood);
        GameWin();
    }

    public void GameWin()
    {
        gameEnded = true;
        UnlockCursor();
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver(string reason)
    {
        gameEnded = true;
        UnlockCursor();
        if (loseReasonText != null) loseReasonText.text = reason;
        if (losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}