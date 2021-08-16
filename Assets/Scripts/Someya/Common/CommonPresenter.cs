//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Cysharp.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//[RequireComponent(typeof(CommonFadeControl))]
//[RequireComponent(typeof(CommonFadeView))]
//[RequireComponent(typeof(MessageManager))]
//public class CommonPresenter : MonoBehaviour
//{
//    private CommonFadeControl _commonFadeControl;
//    private CommonFadeView _commonFadeView;
//    private MessageManager _messageManager;

//    private Dictionary<string, string> _beforeSceneNameStack = new Dictionary<string, string>()
//                                                                     {
//                                                                         {"HomeScene", ""},
//                                                                         {"CardScene", ""},
//                                                                         {"BattleScene", ""},
//                                                                         {"CombineScene", ""},
//                                                                         {"DeckScene", ""},
//                                                                         {"DungeonScene", ""},
//                                                                         {"GimmicScene", ""},
//                                                                         {"QuestScene", ""},
//                                                                         {"ResultScene", ""},
//                                                                         {"TitleScene", ""},
//                                                                     };

//    private void Awake()
//    {
//        _commonFadeControl = GetComponent<CommonFadeControl>();
//        _commonFadeView = GetComponent<CommonFadeView>();
//        _messageManager = GetComponent<MessageManager>();
//    }

//    void Update()
//    {
//        if (Input.GetKey(KeyCode.Escape))
//        {
//            Application.Quit();
//        }
//    }

//    /// <summary>
//    /// ÉVÅ[ÉìëJà⁄
//    /// </summary>
//    /// <param name="sceneName">ëJà⁄Ç∑ÇÈÉVÅ[Éìñº</param>
//    /// <param name="isFirst">èâä˙èÛë‘Ç»ÇÁtrue</param>
//    public async void SceneChange(string sceneName, bool isFirst = false)
//    {
//        _commonFadeView.FadeImage.raycastTarget = true;
//        if (isFirst)
//            _commonFadeView.FadeCanvasGroup.alpha = 1.0f;
//        else
//            await _commonFadeControl.FadeOut(_commonFadeView.FadeCanvasGroup);
//        _beforeSceneNameStack[sceneName] = SceneManager.GetActiveScene().name;
//        await SceneManager.LoadSceneAsync(sceneName);
//        await _commonFadeControl.FadeIn(_commonFadeView.FadeCanvasGroup);
//        _commonFadeView.FadeImage.raycastTarget = false;
//    }

//    public async void ReturnSceneChange()
//    {
//        _commonFadeView.FadeImage.raycastTarget = true;
//        var nowSceneName = SceneManager.GetActiveScene().name;
//        if (!_beforeSceneNameStack.ContainsKey(nowSceneName))
//            nowSceneName = "HomeScene";
//        else if (string.IsNullOrWhiteSpace(_beforeSceneNameStack[nowSceneName]))
//            nowSceneName = "HomeScene";
//        else
//            nowSceneName = _beforeSceneNameStack[nowSceneName];
//        _beforeSceneNameStack[nowSceneName] = "";
//        await _commonFadeControl.FadeOut(_commonFadeView.FadeCanvasGroup);
//        await SceneManager.LoadSceneAsync(nowSceneName);
//        await _commonFadeControl.FadeIn(_commonFadeView.FadeCanvasGroup);
//        _commonFadeView.FadeImage.raycastTarget = false;
//    }

//    public async void FadeIn()
//    {
//        _commonFadeView.FadeCanvasGroup.alpha = 1.0f;
//        await _commonFadeControl.FadeIn(_commonFadeView.FadeCanvasGroup);
//        _commonFadeView.FadeImage.raycastTarget = false;
//    }

//    public int InitMessage(string message, int speed, UnityAction<string, bool> callback)
//    {
//        var index = _messageManager.InitMessage(message, speed);
//        _messageManager.StartDispMessage(index, callback);
//        return index;
//    }

//    public void SkipMessage(int index)
//    {
//        _messageManager.SkipMessage(index);
//    }
//}