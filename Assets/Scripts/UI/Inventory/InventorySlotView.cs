using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;

    public void Show(ItemStack stack)
    {
        if (stack.IsEmpty)
        {
            icon.enabled = false;
            countText.text = string.Empty;
            return;
        }

        icon.enabled = true;
        icon.sprite = stack.Item.Icon;
        countText.text = stack.Count > 1 ? stack.Count.ToString() : string.Empty;
    }
}