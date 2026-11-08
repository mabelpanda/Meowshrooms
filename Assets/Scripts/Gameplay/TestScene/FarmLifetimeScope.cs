using VContainer;
using VContainer.Unity;

// VContainer для сцены: дочерний для RootLifetimeScope (наследует все регистрации у родителя, но не наоборот)
// живет пока жива сцена (фермы, Test) 
public class FarmLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // регестрируем тестер для инвентаря
        builder.RegisterComponentInHierarchy<DebugInventoryTester>();
    }
}
