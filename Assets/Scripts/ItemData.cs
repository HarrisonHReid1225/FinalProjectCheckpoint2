using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStackSize = 5;

    [Header("Consumable Settings")]
    public bool isConsumable;
    public float hungerRestored = 20f;
}