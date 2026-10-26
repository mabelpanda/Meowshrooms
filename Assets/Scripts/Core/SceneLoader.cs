using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene current;

    public IEnumerator SwitchTo(string sceneName)
    {
        if (current.IsValid())
            yield return SceneManager.UnloadSceneAsync(current);

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        current = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(current);
    }
}