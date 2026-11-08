using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject panel;             // фон инвентаря
    [SerializeField] private Transform slotsRoot;          // слоты, Grid Layout Group
    [SerializeField] private InventorySlotView slotPrefab; // шаблон слота

    private Inventory _inventory;
    private readonly List<InventorySlotView> _slotViews = new();

    [Inject]
    public void Construct(Inventory inventory) // псевдоконструктор для VContainer
    {
        _inventory = inventory;
    }

    private void Start()
    {
        for (int i = 0; i < _inventory.Size; i++)
            _slotViews.Add(Instantiate(slotPrefab, slotsRoot)); // заполняем инвентарь

        _inventory.Changed += Refresh; 
        Refresh();
        panel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_inventory != null)
            _inventory.Changed -= Refresh;
    }

    public void Toggle() // функция для кнопки
    {
        panel.SetActive(!panel.activeSelf); // если открыто - закрыть, если закрыто - открыть
    }

    private void Refresh()
    {
        for (int i = 0; i < _slotViews.Count; i++)
            _slotViews[i].Show(_inventory.GetSlot(i));
    }
}
