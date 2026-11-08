using UnityEngine;
using System;

public class Inventory
{
    public event Action Changed;
    // разбор синтаксиса:
    // Action это делегат (из стандартной библиотеки) - ссылка на метод(ы), как std::function в плюсах (+ multicast)
    // event - ключевое слово, оно добавляет подписки (+=)/отписки (-=) к делегату
    // это готовый шаблон: издатель и подписчики
    // смысл: событие - содержимое инвентаря изменилось

    // если короче: мы где то вызываем Add(гриб), этот метод внутри себя вызывает Changed?.Invoke(), после чего весь код
    // который зависит от инвентаря (например UI) актуализирует свои данные в соответствии с новыми изменениями инвентаря.
    // чтобы по сто раз не вызывать методы других классов и точно все учесть - подписываемся на событие
    // (тогда тот же UI внутри СЕБЯ знает, что он должен обновляться когда событие Changed)

    private readonly ItemStack[] _slots; // собственно сам инвентарь - массив из слотов (приватный, меняем через события)
    public int Size => _slots.Length;

    // конструктор
    public Inventory(int size)
    {
        _slots = new ItemStack[size];
    }

    public ItemStack GetSlot(int index) => _slots[index]; // геттер (возвращаем копию, см. ItemStack)

    public int Count(ItemData item)  // сколько всего этого предмета (item) в инвентаре
    {
        int total = 0;
        foreach (ItemStack slot in _slots) // для каждого слота 
        {
            if (slot.Item == item) // ищем слоты в которых лежат наши item'ы
                total += slot.Count; // и считаем общую сумму item'ов
        }
        return total;
    }

    public bool Contains(ItemData item, int amount = 1) // возвращает: содержится ли item в количестве amount штук
    {
        return Count(item) >= amount;
    }

    public int Add(ItemData item, int amount) // метод добавления предмета item в инвентарь в количестве amount штук
    // возвращает int: сколько штук НЕ поместилось
    {
        if (item == null || amount <= 0)
            return amount;

        int remaining = amount; // остаток

        // сначала заполняем уже начатые слоты
        for (int i = 0; i < _slots.Length && remaining > 0; i++)
        {
            if (_slots[i].Item != item) // пропускаем все слоты не с нашим item
                continue;

            int space = item.MaxStack - _slots[i].Count; // сколько осталось свободного места в слоте
            if (space <= 0) 
                continue;

            int toAdd = Math.Min(space, remaining); // добавляем в этот слот либо до предела слота, либо весь остаток
             // заменяем слот на новый тк старый readonly
             // new здесь не создает новый обьект, а перезаписывает (16 байт) внутри массива (особенность struct в c#)
             // поэтому это также дешево как изменить поле
            _slots[i] = new ItemStack(item, _slots[i].Count + toAdd);
            remaining -= toAdd;
        }

        // то что осталось раскладываем по пустым слотам
        for (int i = 0; i < _slots.Length && remaining > 0; i++)
        {
            if (!_slots[i].IsEmpty) 
                continue;

            int toAdd = Math.Min(item.MaxStack, remaining);
            _slots[i] = new ItemStack(item, toAdd);
            remaining -= toAdd;
        }

        if (remaining != amount) // если хоть что то положили,
            Changed?.Invoke(); // сообщаем подписчикам что изменения произошли

        return remaining;
    }

    // метод удаения предметов item в количестве amount штук
    // принцип всё или ничего: если что то не так, то ничего не убирает (false)
    public bool Remove(ItemData item, int amount)
    {
        if (item == null || amount <= 0 || Count(item) < amount) // собственно проверка "что то не так"
            return false;

        int remaining = amount;

        // забираем с конца, чтобы начальные стаки оставались полными
        for (int i = _slots.Length - 1; i >= 0 && remaining > 0; i--)
        {
            if (_slots[i].Item != item)
                continue;

            int toTake = Math.Min(_slots[i].Count, remaining);
            int left = _slots[i].Count - toTake; // остаток
            _slots[i] = left > 0 ? new ItemStack(item, left) : default(ItemStack); // пустой слот, если полностью опустошили слот (остаток 0)
            remaining -= toTake;
        }

        Changed?.Invoke(); // сообщаем подписчикам (проверка с return false уже была)
        return true;
    }
}