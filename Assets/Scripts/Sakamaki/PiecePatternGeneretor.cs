using UnityEngine;

public class PiecePatternGeneretor : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;

    // 数値シャッフルするための変数
    static int SHUFFLE_NUM = 100;
    // シャッフルされた数値格納する変数
    int num = 0;
    public static int type = 0;
    bool whiteChecker = false;
    bool blackChecker = false;
    
    public GameObject Generate(Vector3 GeneratePos)
    {
        // コマタイプ
        type = 0;
        // ランダム
        num = Random.Range(0, SHUFFLE_NUM);

        // 50% で別色
        if (num < 50 || (blackChecker && whiteChecker))
        {
            type = 3;

            blackChecker = false;
            whiteChecker = false;
        }
        // 25% で同色 (白)
        else if ((num < 75 && !whiteChecker) || blackChecker)
        {
            type = 2;
            
            whiteChecker = true;
        }
        // 余りの25%　(黒)
        else
        {
            type = 1;
            
            blackChecker = true;
        }

        // コマ1つ目処理
        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        piece.transform.position = GeneratePos;
        Piece p1 = piece.GetComponent<Piece>();
        
        // 2つ目処理
        GameObject piece2 = Instantiate(piecePrefab);
        piece2.transform.parent = root.transform;
        piece2.transform.position = GameDirector.Instance._DEFAULT_POSITION + Vector3.forward + new Vector3(0, 0, 1);
        Piece p2 = piece2.GetComponent<Piece>();

        switch (type)
        {
            // 黒同色タイプ
            case 1:
                piece.name = "black";
                piece2.name = "black";
                p1.pieceType = Piece.PieceType.black;
                p2.pieceType = Piece.PieceType.black;
                break;
            // 白同色
            case 2:
                piece.name = "white";
                piece2.name = "white";
                p1.pieceType = Piece.PieceType.white;
                p2.pieceType = Piece.PieceType.white;
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                piece2.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            // 黒白
            case 3:
                piece.name = "black";
                piece2.name = "white";
                p1.pieceType = Piece.PieceType.black;
                p2.pieceType = Piece.PieceType.white;
                piece2.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                Debug.LogError("想定されていない値が代入されました。");
                break;
        }

        // 代入
        GameDirector.Instance._activePieces[0] = piece;
        GameDirector.Instance._activePieces[1] = piece2;

        return null;
    }
}