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
    //private byte generateCount = 0;

    void Start()
    {

        // 最初に全て生成した方がいいかも
    }

    void Update()
    {
        if (minoController.isLanding)
        {
            GameObject piece1 = Generate(0);
            GameObject piece2 = Generate(1);
            piece1.transform.position = DEFAULT_POSITION;
            piece2.transform.position = DEFAULT_POSITION + new Vector3(0, 0, 1);

            minoController.controllPieces[0] = piece1;
            minoController.controllPieces[1] = piece2;
            minoController.isLanding = false;
        }
    }

    private GameObject Generate(int color)
    {
        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        switch (color)
        {
            case 0:
                piece.name = "black";
                // pieceスクリプトに色々書きこむ
                break;
            case 1:
                piece.name = "white";
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                break;
        }
        return piece;
    }
}

