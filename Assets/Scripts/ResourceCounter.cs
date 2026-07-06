using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
{
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI pearText;

    private int apples;
    private int ores;
    private int pears;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        appleText.text = "Apples: " + apples;
        oreText.text = "Ores: " + ores;
        pearText.text = "Pears: " + pears;
    }

    public void AddResource(string resourceName, int amount)
    {
        if (resourceName == "Apple")
        {
            apples += amount;
        }
        else if (resourceName == "Ore")
        {
            ores += amount;
        }
        else if (resourceName == "Pear")
        {
            pears += amount;
        }

        UpdateUI();
    }
}