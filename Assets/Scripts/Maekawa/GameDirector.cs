using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;
    [SerializeField]
    private MinoController minoController = null;

    private Vector3 DEFAULT_POSITION = new Vector3(1, 0, -1);
    public static bool isGenerate = true;
    //private byte generateCount = 0;

    void Start()
    {
        isGenerate = true;
        // 最初に全て生成した方がいいかも
    }

    void Update()
    {
        if (isGenerate)
        {
            GameObject piece1 = Generate(1);
            GameObject piece2 = Generate(2);
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

