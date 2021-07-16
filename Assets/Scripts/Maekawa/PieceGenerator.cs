using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;
    private int _range = 0;
    private int _whiteCount, _blackCount, _jokerCount = 0;
    private const int _WHITE_COUNT_MAX = 15;
    private const int _BLACK_COUNT_MAX = 15;
    private const int _JOKER_COUNT_MAX = 0;// 一旦jokerなし

    private void Start()
    {
        _range = _WHITE_COUNT_MAX + _BLACK_COUNT_MAX;//+ _JOKER_COUNT_MAX;
    }

    public GameObject Generate(Vector3 GeneratePos)
    {
        // コマタイプ
        int type;

        // 母数が減るボックスガチャ形式でジョーカーを抽選
        int num = Random.Range(0, _range);
        // 0 ~ 99で黒と白抽選区分
        int num2 = Random.Range(0, 1000);

        // 生成ごとに減少する_renge変数が0 & 生成上限に達していなければジョーカー
        if (num == 0 && _jokerCount < _JOKER_COUNT_MAX)
        {
            type = 3;
            _jokerCount++;
        }
        // 余りが0 & 黒の生成数が上限に達していない　or 白の生成数が上限に達しているなら黒
        else if ((num2 % 2 == 0 && _blackCount < _BLACK_COUNT_MAX) || _whiteCount >= _WHITE_COUNT_MAX)
        {
            type = 1;
            _blackCount++;
        }
        // 上記に該当しなければ白
        else
        {
            type = 2;
            _whiteCount++;
        }

        // 一回抽選したら範囲を狭める
        _range--;

        // 範囲が0になった時にリセット処理
        if (_range == 0)
        {
            Debug.Log($"<color=orange>Joker : {_jokerCount} B : {_blackCount} W : {_whiteCount} </color>");
            _range = _WHITE_COUNT_MAX + _BLACK_COUNT_MAX;//+ _JOKER_COUNT_MAX;
            _jokerCount = 0;
            _blackCount = 0;
            _whiteCount = 0;
        }

        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        piece.transform.position = GeneratePos;
        Piece p = piece.GetComponent<Piece>();

        switch (type)
        {
            case 1:
                piece.name = "black";
                p.pieceType = Piece.PieceType.black;
                break;
            case 2:
                piece.name = "white";
                p.pieceType = Piece.PieceType.white;
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            //case 3:
            //    piece.name = "joker";
            //    p.pieceType = Piece.PieceType.joker;
                //break;
            default:
                break;
        }

        return piece;
    }
}
