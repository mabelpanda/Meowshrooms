using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private string firstScene = "Test";

    private IEnumerator Start()
    {
        // позже сюда добавим: создание инвентаря, загрузку сохранения
        yield return sceneLoader.SwitchTo(firstScene);
    }
}
