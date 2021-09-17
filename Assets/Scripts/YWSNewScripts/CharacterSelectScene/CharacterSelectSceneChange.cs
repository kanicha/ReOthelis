using UnityEngine;
public class CharacterSelectSceneChange : SingletonMonoBehaviour<CharacterSelectSceneChange>
{
    private GameSceneManager _gameSceneManager;
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
            SceneChange(_gameSceneManager);
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("GameSceme");
    }
}
