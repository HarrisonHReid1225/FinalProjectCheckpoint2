using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlotUI[] slotUIs;
    public InventorySlotData[] slots;
    public PlayerStats playerStats;

    void Start()
    {
        slots = new InventorySlotData[slotUIs.Length];

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slotUIs[i] != null)
            {
                slotUIs[i].Initialize(this, i);
            }
        }

        UpdateUI();
    }

    public void AddItem(ItemData item, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            if (slots[i].item == item && slots[i].amount < item.maxStackSize)
            {
                int spaceLeft = item.maxStackSize - slots[i].amount;
                int amountToAdd = Mathf.Min(spaceLeft, amount);

                slots[i].amount += amountToAdd;
                amount -= amountToAdd;

                if (amount <= 0)
                {
                    UpdateUI();
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null) continue;

            int amountToAdd = Mathf.Min(item.maxStackSize, amount);
            slots[i] = new InventorySlotData(item, amountToAdd);
            amount -= amountToAdd;

            if (amount <= 0)
            {
                UpdateUI();
                return;
            }
        }

        UpdateUI();
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= slots.Length || slots[index] == null) return;

        ItemData item = slots[index].item;

        if (item.isConsumable && playerStats != null)
        {
            playerStats.EatFood(item.hungerRestored);

            slots[index].amount--;
            if (slots[index].amount <= 0)
            {
                slots[index] = null;
            }

            UpdateUI();
        }
    }

    public int GetItemCount(ItemData item)
    {
        int total = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].item == item)
            {
                total += slots[i].amount;
            }
        }
        return total;
    }

    public void RemoveItem(ItemData item, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].item == item)
            {
                if (slots[i].amount > amount)
                {
                    slots[i].amount -= amount;
                    UpdateUI();
                    return;
                }
                else
                {
                    amount -= slots[i].amount;
                    slots[i] = null;
                }
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slots[i] == null)
            {
                slotUIs[i].ClearSlot();
            }
            else
            {
                slotUIs[i].SetSlot(slots[i].item, slots[i].amount);
            }
        }
    }
}