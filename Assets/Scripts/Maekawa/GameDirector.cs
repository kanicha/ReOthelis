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
    private const int _DEFAULT_RANGE = 30;
    private int _range = 30;
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
            int[] type = new int[2];
            for (int i = 0; i < 2; i++)
            {
                int num = Random.Range(0, _range);
                int num2 = Random.Range(0, 100);
                if (num == _range && _jokerCount < _JOKER_COUNT_MAX)
                {
                    type[i] = 3;
                    _jokerCount++;
                }
                else if (num2 % 2 == 0 && _blackCount <= _BLACK_COUNT_MAX)
                {
                    type[i] = 1;
                    _blackCount++;
                }
                else /*if (num % 2 == 1 && _whiteCount < _WHITE_COUNT_MAX)*/
                {
                    type[i] = 2;
                    _whiteCount++;
                }
                _range--;

                if (_range <= 0)
                {
                    _range = _DEFAULT_RANGE;
                    _jokerCount = _JOKER_COUNT_MAX;
                    _blackCount = _BLACK_COUNT_MAX;
                    _whiteCount = _WHITE_COUNT_MAX;

                    Debug.Log("aaaa");
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