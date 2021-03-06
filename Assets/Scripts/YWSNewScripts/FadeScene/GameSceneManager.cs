using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(FadeManager))]
public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private Enum _fadeType;
    private string nowSceneName = "";
    private FadeManager _fadeManager;
    public bool IsChanged = false;
    
    public IEnumerator SceneChange(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(nowSceneName))
        {
            yield return SceneManager.UnloadSceneAsync(nowSceneName);
        }

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        nowSceneName = sceneName;
    }

    IEnumerator Start()
    {
        _fadeManager = GetComponent<FadeManager>();
        yield return SceneChange("Title");
        yield return _fadeManager.FadeIn(_fadeManager.FadeCanvasGroup, _fadeManager.FadeImage);
        IsChanged = true;
    }

    public void SceneNextCall(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        IsChanged = false;
        yield return _fadeManager.FadeOut(_fadeManager.FadeCanvasGroup, _fadeManager.FadeImage);
        yield return SceneChange(sceneName);
        yield return _fadeManager.FadeIn(_fadeManager.FadeCanvasGroup, _fadeManager.FadeImage);
        IsChanged = true;
    }
}