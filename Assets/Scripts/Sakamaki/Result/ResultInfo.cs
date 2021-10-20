using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultInfo : MonoBehaviour
{
    [SerializeField, Header("対象プレイヤー")] private Player _player;
    [SerializeField, Header("基本スコア")] private Text _preScoreText = null;
    [SerializeField, Header("コマ数スコア")] private Text _reverseScoreText = null;
    [SerializeField, Header("キャラクターの名前オブジェクト")] private Image _charaName;
    [SerializeField, Header("キャラクターの名前画像")] private Sprite[] _charaNameImage = new Sprite[4];
    [SerializeField, Header("キャラクターのオブジェクト")] private Image _standChara;
    [SerializeField, Header("キャラクターの立ち絵画像")] private Sprite[] _standCharaImage = new Sprite[4];
    enum Player
    {
        none,
        Player1,
        Player2
    }

    // Start is called before the first frame update
    void Awake()
    {
        InfoDraw();
    }

    private void InfoDraw()
    {
        switch (_player)
        {
            case Player.Player1:
                _preScoreText.text = Player_1.displayPreScore.ToString();
                _reverseScoreText.text = Player_1.displayReverseScore.ToString();
                _charaName.sprite = _charaNameImage[(int) CharaImageMoved.charaType1P];
                _standChara.sprite = _standCharaImage[(int) CharaImageMoved.charaType1P];
                break;
            case Player.Player2:
                _preScoreText.text = Player_2.displayPreScore.ToString();
                _reverseScoreText.text = Player_2.displayReverseScore.ToString();
                _charaName.sprite = _charaNameImage[(int) CharaImageMoved2P.charaType2P];
                _standChara.sprite = _standCharaImage[(int) CharaImageMoved2P.charaType2P];
                break;
            default:
                break;
        }
    }
}