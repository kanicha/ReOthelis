using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSample : SingletonMonoBehaviour<MonoBehaviour>
{
    [SerializeField]
    private float waittime = 1;

    [SerializeField]
    private GameObject fadeObj;

    private GameObject _fadePanelObj;
    private Canvas _fadeCanvas;
    private CanvasGroup _fadeCanvasGroup;

    public enum SceneName
    {
        TitleScenes = 0,
        GameScenes,
        Result
    }

    protected override void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //�A�ő΍�
    private bool doOnceSceneChange = true;

    public void SceneLoad(SceneName sceneName, bool isFade = false, float waitTime = -1)
    {
        if (doOnceSceneChange == false) return;

        if (waitTime.Equals(-1)) waitTime = waittime;

        if (doOnceSceneChange)
        {
            StartCoroutine(LoadSceneCor(sceneName, isFade, waitTime));
        }
    }

    private IEnumerator LoadSceneCor(SceneName sceneName, bool isFade, float waitTime)
    {
        // �t�F�[�h�w�肪��������A�j���[�V��������
        if (isFade)
        {
            yield return StartCoroutine(FadeIn());
        }

        doOnceSceneChange = false;
        var async = SceneManager.LoadSceneAsync(sceneName.ToString());
        async.allowSceneActivation = false;
        // �����~�܂��Ă��邱�Ƃ��l�����A�Ƃ肠�����߂�
        Time.timeScale = 1;

        yield return new WaitForSecondsRealtime(waitTime);

        doOnceSceneChange = true;
        async.allowSceneActivation = true;

        if (isFade)
        {
            yield return StartCoroutine(FadeOut());
        }
    }

    /// <summary>
    /// �t�F�[�h�ɕK�v�ȃI�u�W�F�N�g�𐶐�����
    /// </summary>
    private void InitFadeObj()
    {
        // Canvas����
        _fadePanelObj = new GameObject("FadePanel");
        _fadeCanvas = _fadePanelObj.AddComponent<Canvas>();
        _fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _fadeCanvas.sortingOrder = 99;

        // Fade�I�u�W�F�N�g����
        var fadeBody = Instantiate(fadeObj, _fadeCanvas.transform);
        _fadeCanvasGroup = fadeBody.GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// �t�F�[�h�C�������s����
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        if (_fadeCanvasGroup == null) InitFadeObj();

        _fadeCanvasGroup.alpha = 0;

        while (_fadeCanvasGroup.alpha < 1)
        {
            _fadeCanvasGroup.alpha += Time.fixedUnscaledDeltaTime / waittime;

            yield return null;
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�����s����
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        // �V�[�����J�ڂ��ăt�F�[�h�I�u�W�F�N�g���j�������̂�ҋ@
        yield return new WaitUntil(() => _fadeCanvasGroup == null);

        // �t�F�[�h�I�u�W�F�N�g�Đ���
        InitFadeObj();

        _fadeCanvasGroup.alpha = 1;

        while (_fadeCanvasGroup.alpha > 0)
        {
            _fadeCanvasGroup.alpha -= Time.fixedUnscaledDeltaTime / waittime;

            yield return null;
        }
    }
}
