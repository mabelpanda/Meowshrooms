using System.Diagnostics;
using UnityEngine;
using VContainer;

public class DebugInventoryTester : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField, Min(1)] private int amount = 5;

    private Inventory _inventory;

    [Inject]
    public void Construct(Inventory inventory)
    {
        _inventory = inventory;
        UnityEngine.Debug.Log("DebugInventoryTester: Inventory получен");
    }

    private void Start()
    {
        _inventory.Changed += LogInventory;
    }

    private void OnDestroy()
    {
        if (_inventory != null)
            _inventory.Changed -= LogInventory;
    }

    [ContextMenu("Add")]
    private void Add()
    {
        int left = _inventory.Add(item, amount);
        UnityEngine.Debug.Log($"Add {item.DisplayName} x{amount}, не поместилось: {left}");
    }

    [ContextMenu("Remove")]
    private void Remove()
    {
        bool ok = _inventory.Remove(item, amount);
        UnityEngine.Debug.Log($"Remove {item.DisplayName} x{amount}: {ok}");
    }

    private void LogInventory()
    {
        for (int i = 0; i < _inventory.Size; i++)
        {
            ItemStack slot = _inventory.GetSlot(i);
            if (!slot.IsEmpty)
                UnityEngine.Debug.Log($"Слот {i}: {slot.Item.DisplayName} x{slot.Count}");
        }
    }
}