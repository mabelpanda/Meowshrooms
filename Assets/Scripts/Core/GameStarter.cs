using VContainer.Unity;

public class GameStarter : IStartable
{
    private readonly SceneLoader _sceneLoader;
    private readonly string _firstScene;

    public GameStarter(SceneLoader sceneLoader, string firstScene)
    {
        _sceneLoader = sceneLoader;
        _firstScene = firstScene;
    }

    public void Start()
    {
        _sceneLoader.LoadScene(_firstScene);
    }
}