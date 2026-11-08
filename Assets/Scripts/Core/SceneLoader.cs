using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene current;

    public IEnumerator SwitchTo(string sceneName)  // корутина переключения, асинхронность
    {
        if (current.IsValid())
            yield return SceneManager.UnloadSceneAsync(current); // юнитивская корутина
        // итог: каждый кадр (то есть каждый новый вызов этого кода) выполняется корутина UnloadSceneAsync (выгрузка сцены, в фоне, типа пошагово),
        // пока внутри нее не будет isDone == true. (а между шагами выполняется остальной код (в проекте в целом))

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); // Additive добавляет сцену к уже загруженным
        // здесь: когда вырузка сцены UnloadSceneAsync завершилась, идем дальше и выполняем LoadSceneAsync (загрузка сцены)
        // (снова пошаговое выполнение каждый кадр, текущая сцена обрабатывается, снова ждем до isDone)

        // когда сцена загружена:
        current = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(current); // делаем сцену активной
    }

    public void LoadScene(string sceneName) // интерфейс для смены сцен
    {
        StartCoroutine(SwitchTo(sceneName));
    }
}