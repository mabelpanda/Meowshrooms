using UnityEngine;
using VContainer;
using VContainer.Unity;

// Корневой VContainer - управление зависимостями (в т.ч между сценами)
public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private string firstScene = "Test";
    [SerializeField, Min(1)] private int inventorySize = 20;

    protected override void Configure(IContainerBuilder builder) // регистрация контейнером
    {
        builder.Register<Inventory>(Lifetime.Singleton).WithParameter(inventorySize); // регестрируем зависимость (Singleton - один обьект на весь код)
        // когда кто-то потребует Inventory: контейнер вызывает конструктор Inventory(inventorySize) и отдает этот обьект   
        builder.RegisterComponent(sceneLoader); // регистрируем зависимость - компонент из сцены sceneLoader
        // точка входа (потребитель)
        builder.RegisterEntryPoint<GameStarter>().WithParameter(firstScene); // сам контейнер создает обьект GameStarter(firstScene) по конструктору
        
        // для отображения инвентаря
        builder.RegisterComponentInHierarchy<InventoryWindow>();
    }
}