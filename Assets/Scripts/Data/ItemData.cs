using UnityEngine;

// наследование: абстрактный класс-родитель, от него будут наследоваться другие обьекты игры, но обьекты именно этого класса нельзя создать
public abstract class ItemData : ScriptableObject 
{
    // инкапсуляция: private set значит что в коде (вне класса менять) данные нельзя, только в инспекторе
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField, Min(1)] public int MaxStack { get; private set; } = 99;
    
    //Справка по C#: все члены класса здесь:
    //Свойство - поле данных + геттер и сеттер. Запись {get;set;} задает геттеры и сеттеры автоматически/по умолчанию
}