using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public TextMeshProUGUI stackText;

    private int slotIndex;
    private Inventory inventory;

    public void Initialize(Inventory inv, int index)
    {
        inventory = inv;
        slotIndex = index;
    }

    public void SetSlot(ItemData item, int amount)
    {
        itemIcon.sprite = item.icon;
        itemIcon.gameObject.SetActive(true);

        stackText.text = amount.ToString();
        stackText.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.gameObject.SetActive(false);

        stackText.text = "";
        stackText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory != null)
        {
            inventory.UseItem(slotIndex);
        }
    }
}