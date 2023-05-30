using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadNextLevel(sceneName));
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }

    private IEnumerator LoadNextLevel(string sceneName)
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
