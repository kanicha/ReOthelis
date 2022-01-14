using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectSceneChange : SingletonMonoBehaviour<CharacterSelectSceneChange>
{
    private GameSceneManager _gameSceneManager;
    [SerializeField, Header("二人が確定してから遷移する時間")] float _waitTime = 0f;

    [SerializeField]
    private CharaImageMoved _CIM1 = null;
    [SerializeField]
    private CharaImageMoved2P _CIM2 = null;
    public bool isLoading = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _CIM1 = FindObjectOfType<CharaImageMoved>();
        _CIM2 = FindObjectOfType<CharaImageMoved2P>();
    }

    void Update()
    {
        if (isLoading)
            return;

        // 1p&2pの確定を待ってから
        if (_gameSceneManager.IsChanged == true && _CIM1.isConfirm && _CIM2.isConfirm)
        {
            isLoading = true;

            StartCoroutine(WaitChange());
        }
    }

    IEnumerator WaitChange()
    {
        // 秒数まつ
        yield return new WaitForSeconds(_waitTime);

        SceneChange(_gameSceneManager);
    }

    //次のシーンに進む
    private void SceneChange(GameSceneManager gameSceneManager)
    {
        if (ModeSelect._selectCount == 0)
        {
            SoundManager.Instance.StopBGM();
            
            gameSceneManager.SceneNextCall("CharacterScenario_PA");
        }
        else
        {
            gameSceneManager.SceneNextCall("GameSceme");
        }
    }

    
}
