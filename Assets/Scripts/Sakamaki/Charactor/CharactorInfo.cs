using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorInfo : MonoBehaviour
{
    [SerializeField, Header("対象プレイヤー")] private Player _player;
    [SerializeField, Header("キャラクターの名前オブジェクト")] private Image _charaName;
    [SerializeField, Header("キャラクターの名前画像")] private Sprite[] _charaNameImage = new Sprite[4];
    [SerializeField, Header("スキルのオブジェクト")] private GameObject[] _skillObject = new GameObject[4];
    [SerializeField, Header("OKボタンオブジェクト")] private GameObject _okButton;
    [SerializeField, Header("OKボタン画像")] private Sprite[] _okButtonImage = new Sprite[4];

    private Animator _animator;
    enum Player
    {
        none,
        Player1,
        Player2
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InfoDraw();
    }

    public void InfoDraw()
    {
        // オブジェクトを非アクティブに
        for (int i = 0; i < _skillObject.Length; i++)
        {
            _skillObject[i].SetActive(false);
        }
        
        switch (_player)
        {
            case Player.Player1:
                _charaName.sprite = _charaNameImage[(int) CharaImageMoved.charaType1P];
                _okButton.GetComponent<Image>().sprite = _okButtonImage[(int) CharaImageMoved.charaType1P];
                _skillObject[(int) CharaImageMoved.charaType1P].SetActive(true);
                break;
            case Player.Player2:
                _charaName.sprite = _charaNameImage[(int) CharaImageMoved2P.charaType2P];
                _okButton.GetComponent<Image>().sprite = _okButtonImage[(int) CharaImageMoved2P.charaType2P];
                _skillObject[(int) CharaImageMoved2P.charaType2P].SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// OKボタンのアニメーション処理
    /// </summary>
    public void OKButtonAnimControl()
    {
        // 初期化
        _animator = _okButton.GetComponent<Animator>();
        
        _animator.SetTrigger("okButton");
    }
}
