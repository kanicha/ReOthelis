using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;
    [SerializeField]
    private MinoController minoController = null;
    [SerializeField, Header("ミノの初期位置")]
    Vector3 DEFAULT_POSITION = Vector3.zero;
    public static bool isGenerate = true;
    //private byte generateCount = 0;
    
    // Jokerコマを含めると(黒15 白15 Joker + 1 ) * 2(2セット)なので 31で宣言
    private const int _DEFAULT_RANGE = 31;
    private int _range = 31;
    private int _whiteCount, _blackCount, _jokerCount = 0;
    private const int _WHITE_COUNT_MAX = 15;
    private const int _BLACK_COUNT_MAX = 15;
    private const int _JOKER_COUNT_MAX = 1;

    void Start()
    {
        isGenerate = true;
        // 最初に全て生成した方がいいかも
    }

    void Update()
    {
        if (isGenerate)
        {
            // コマタイプ
            int[] type = new int[2];
            // for文で回し2つ分生成
            for (int i = 0; i < 2; i++)
            {
                // Randomし0 ~ 31の抽選
                int num = Random.Range(0, _range + 1);
                // 0 ~ 99を使い黒と白抽選区分
                int num2 = Random.Range(0, 100);
                
                // Jokerコマ生成
                if (num == _range && _jokerCount < _JOKER_COUNT_MAX)
                {
                    type[i] = 3;
                    _jokerCount++;
                }
                // 黒コマ生成 (1)
                else if (num2 % 2 == 0 && (_blackCount < _BLACK_COUNT_MAX
                    || _whiteCount > _WHITE_COUNT_MAX))
                {
                    type[i] = 1;
                    _blackCount++;
                }
                // 白コマ生成 (2)
                else 
                {
                    type[i] = 2;
                    _whiteCount++;
                }
                // 一回抽選したら範囲を狭める
                _range--;
                
                // 範囲が0になった時にリセット処理
                if (_range <= 0)
                {
                    // 各コマ生成数Debug.Log
                    Debug.Log("Joker: " + _jokerCount + "\n");
                    Debug.Log("Black: " + _blackCount + "\n");
                    Debug.Log("White: " + _whiteCount + "\n");
                    
                    _range = _DEFAULT_RANGE;
                    _jokerCount = _JOKER_COUNT_MAX;
                    _blackCount = _BLACK_COUNT_MAX;
                    _whiteCount = _WHITE_COUNT_MAX;

                    Debug.Log("Reset");
                }
            }
            
            GameObject piece1 = Generate(type[0]);
            GameObject piece2 = Generate(type[1]);

            piece1.transform.position = DEFAULT_POSITION;
            piece2.transform.position = DEFAULT_POSITION + new Vector3(0, 0, 1);

            minoController.controllPieces[0] = piece1;
            minoController.controllPieces[1] = piece2;
            isGenerate = false;
        }
    }

    private GameObject Generate(int color)
    {
        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        Piece p = piece.GetComponent<Piece>();
        minoController.rotationNum = 0;

        switch (color)
        {
            case 1:
                piece.name = "black";
                p.pieceType = Piece.PieceType.black;
                // pieceスクリプトに色々書きこむ
                break;
            case 2:
                piece.name = "white";
                p.pieceType = Piece.PieceType.white;
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                piece.name = "joker";
                p.pieceType = Piece.PieceType.joker;
                break;
            default:
                break;
        }

        return piece;
    }
}