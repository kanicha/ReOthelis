using UnityEngine;

public class MinoController : MonoBehaviour
{
    [SerializeField]
    private float fallTime = 1f;
    [SerializeField]
    private Map _map = null;
    public GameObject[] controllPieces = new GameObject[2];
    private Piece.PieceType playersType = Piece.PieceType.black;// 仮で常に黒プレイヤ
    private bool _isInput = false;
    private bool _isFalled = false;
    public int rotationNum = 0;// 左回転
    private Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,  0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0,  0, -1),
        new Vector3(1,  0, 0)
    };

    void Update()
    {
        if (!GameDirector.isGenerate)
        {
            _isInput = false;
            Rotate();
            Move();

            if(_isInput)
            {
                if (_map.CheckLanding(controllPieces[0].transform.position))
                {
                    _map.FallPiece(controllPieces[0]);
                    _map.FallPiece(controllPieces[1]);
                    _isFalled = true;
                }
                else if (_map.CheckLanding(controllPieces[1].transform.position))
                {
                    // 回転側が接地したら回転側から落とす
                    _map.FallPiece(controllPieces[1]);
                    _map.FallPiece(controllPieces[0]);
                    _isFalled = true;
                }

                if (_isFalled)
                {
                    // どちらのコマから関数を呼ぶか判定
                    GameObject priorityPiece;
                    GameObject nonPriorityPiece;
                    Piece piece1 = controllPieces[0].GetComponent<Piece>();
                    Piece piece2 = controllPieces[1].GetComponent<Piece>();

                    // [色を比較]  どちらも自分の色、どちらも相手の色ならポジションで判断する
                    if (piece1.pieceType == playersType && piece2.pieceType == playersType || piece1.pieceType != playersType && piece2.pieceType != playersType)
                    {
                        // 高さが同じなら
                        if ((int)controllPieces[0].transform.position.z == (int)controllPieces[1].transform.position.z)
                        {
                            // 左側のコマから処理
                            if (controllPieces[0].transform.position.x < controllPieces[1].transform.position.x)
                            {
                                priorityPiece = controllPieces[0];
                                nonPriorityPiece = controllPieces[1];
                            }
                            else
                            {
                                priorityPiece = controllPieces[1];
                                nonPriorityPiece = controllPieces[0];
                            }
                        }
                        // 下側のコマから処理
                        else if (controllPieces[0].transform.position.z < controllPieces[1].transform.position.z)
                        {
                            priorityPiece = controllPieces[0];
                            nonPriorityPiece = controllPieces[1];
                        }
                        else
                        {
                            priorityPiece = controllPieces[1];
                            nonPriorityPiece = controllPieces[0];
                        }
                    }
                    else if(piece1.pieceType == playersType)
                    {
                        priorityPiece = controllPieces[0];
                        nonPriorityPiece = controllPieces[1];
                    }
                    else
                    {
                        priorityPiece = controllPieces[1];
                        nonPriorityPiece = controllPieces[0];
                    }

                    _map.CheckReverse(priorityPiece);
                    _map.CheckReverse(nonPriorityPiece);

                    GameDirector.isGenerate = true;
                    _isFalled = false;
                }
            }
        }
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.A))
            move.x = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            move.x = 1;
        else if (Input.GetKeyDown(KeyCode.S))
            move.z = -1;
        // 時間落下
        //else if (Time.time - previousTime >= fallTime)
        //{
        //    move.y = -1;

        //    previousTime = Time.time;

        //    // S入力すると落ちるスピードアップ
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        fallTime = (float)0.1;
        //    }
        //}

        Vector3 movedPos = controllPieces[0].transform.position + move;
        Vector3 rotMovedPos = rotationPos[rotationNum] + movedPos;

        if (_map.CheckWall(movedPos) && _map.CheckWall(rotMovedPos))
        {
            controllPieces[0].transform.position = movedPos;
            controllPieces[1].transform.position = rotMovedPos;
            _isInput = true;
        }
    }

    private void Rotate()
    {
        int lastNum = rotationNum;
        // 左回転
        if (Input.GetKeyDown(KeyCode.Q))
            rotationNum++;
        // 右回転(=左に3回転)
        else if (Input.GetKeyDown(KeyCode.E))
            rotationNum += 3;

        // 疑似回転(移動がややこしくなるのでRotationはいじらない)
        rotationNum %= 4;
        Vector3 rotatedPos = controllPieces[0].transform.position + rotationPos[rotationNum];

        if (_map.CheckWall(rotatedPos))
        {
            _isInput = true;
            controllPieces[1].transform.position = rotatedPos;
        }
        else
            rotationNum = lastNum;
    }
}