using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaingameCharaInfo : MonoBehaviour
{
    [SerializeField, Header("対象プレイヤー")] private Player _player;
    [SerializeField, Header("キャラクターのオブジェクト")] private Image _charaName;
    [SerializeField, Header("キャラクターの画像")] private Sprite[] _charaImage = new Sprite[4];
    [SerializeField, Header("スキルのオブジェクト")] private GameObject[] _skillObject = new GameObject[4];
    
    enum Player
    {
        none,
        Player1,
        Player2
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトを非アクティブに
        for (int i = 0; i < _skillObject.Length; i++)
        {
            _skillObject[i].SetActive(false);
        }
        
        switch (_player)
        {
            case Player.Player1:
                _charaName.sprite = _charaImage[(int) CharaImageMoved.charaType1P];
                _skillObject[(int) CharaImageMoved.charaType1P].SetActive(true);
                break;
            case Player.Player2:
                _charaName.sprite = _charaImage[(int) CharaImageMoved2P.charaType2P];
                _skillObject[(int) CharaImageMoved2P.charaType2P].SetActive(true);
                break;
            default:
                break;
        }
    }
}
