using UnityEngine;

public class PiecePatternGeneretorForT : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;

    public static int type = 0;
    private int PieceGenerated = 0;

    public GameObject Generate(Vector3 GeneratePos)
    {
        // コマタイプ
        type = 0;

        // 別色
        if (PieceGenerated == 0)
        {
            type = 3;
            PieceGenerated++;
        }
        // (黒)
        else if (PieceGenerated == 1)
        {
            type = 1;
            PieceGenerated = 0;
        }

        // コマ1つ目処理
        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        piece.transform.position = GeneratePos;
        Piece p1 = piece.GetComponent<Piece>();
        
        // 2つ目処理
        GameObject piece2 = Instantiate(piecePrefab);
        piece2.transform.parent = root.transform;
        piece2.transform.position = TutorialDirector.Instance._DEFAULT_POSITION + Vector3.forward + new Vector3(0, 0, 1);
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
        TutorialDirector.Instance._activePieces[0] = piece;
        TutorialDirector.Instance._activePieces[1] = piece2;

        return null;
    }

    public void InitPieceMap()
    {
        for (int i = 1; i < 9; i++)
        {
            if (i % 2 == 0)
            {
                GameObject blackPiece = Instantiate(piecePrefab);
                blackPiece.transform.parent = root.transform;
                blackPiece.transform.position = new Vector3(i, 0, -8);
                Piece bp = blackPiece.GetComponent<Piece>();
                blackPiece.name = "black";
                bp.pieceType = Piece.PieceType.black;
                TutorialMap.Instance.pieceMap[8, i] = blackPiece;
            }
            else if (i % 2 == 1)
            {
                GameObject whitePiece = Instantiate(piecePrefab);
                whitePiece.transform.parent = root.transform;
                whitePiece.transform.position = new Vector3(i, 0, -8);
                Piece wp = whitePiece.GetComponent<Piece>();
                whitePiece.name = "white";
                wp.pieceType = Piece.PieceType.white;
                whitePiece.transform.rotation = Quaternion.Euler(0, 0, 180);
                TutorialMap.Instance.pieceMap[8, i] = whitePiece;
            }
        }
    }
}