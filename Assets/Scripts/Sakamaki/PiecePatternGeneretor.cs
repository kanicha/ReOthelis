using UnityEngine;

public class PiecePatternGeneretor : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;

    // ���l�V���b�t�����邽�߂̕ϐ�
    static int SHUFFLE_NUM = 100;
    // �V���b�t�����ꂽ���l�i�[����ϐ�
    int num = 0;

    bool whiteChecker = false;
    bool blackChecker = false;


    private void Start()
    {

    }

    public GameObject Generate(Vector3 GeneratePos)
    {
        // �R�}�^�C�v
        int type = 0;
        // �����_��
        num = Random.Range(0, SHUFFLE_NUM);

        // 50% �ŕʐF
        if (num < 50 || (blackChecker && whiteChecker))
        {
            type = 3;

            blackChecker = false;
            whiteChecker = false;
        }
        // 25% �œ��F (��)
        else if ((num < 75 && !whiteChecker) || blackChecker)
        {
            type = 2;
            whiteChecker = true;
        }
        // �]���25%�@(��)
        else
        {
            type = 1;
            blackChecker = true;
        }

        GameObject piece = Instantiate(piecePrefab);
        GameObject piece2 = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        piece2.transform.parent = root.transform;
        piece.transform.position = GeneratePos;
        piece2.transform.position = GeneratePos + new Vector3(0, 0, 1);
        Piece p1 = piece.GetComponent<Piece>();
        Piece p2 = piece2.GetComponent<Piece>();

        switch (type)
        {
            // �����F�^�C�v
            case 1:
                piece.name = "black";
                piece2.name = "black";
                p1.pieceType = Piece.PieceType.black;
                p2.pieceType = Piece.PieceType.black;
                break;
            // �����F
            case 2:
                piece.name = "white";
                piece2.name = "white";
                p1.pieceType = Piece.PieceType.white;
                p2.pieceType = Piece.PieceType.white;
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                piece2.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            // ����
            case 3:
                piece.name = "black";
                piece2.name = "white";
                p1.pieceType = Piece.PieceType.black;
                p2.pieceType = Piece.PieceType.white;
                piece2.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                break;
        }

        // ���
        GameDirector.Instance._activePieces[0] = piece;
        GameDirector.Instance._activePieces[1] = piece2;

        return null;
    }
}