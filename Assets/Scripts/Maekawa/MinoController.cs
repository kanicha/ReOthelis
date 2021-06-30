using UnityEngine;
using UnityEngine.UI;

public class MinoController : MonoBehaviour
{
    [SerializeField]
    private int moveSpeed = 40;
    [SerializeField]
    private float fallTime = 1f;
    [SerializeField]
    private Map _map = null;
    [SerializeField]
    private Player1 p1 = null;
    [SerializeField]
    private Player2 p2 = null;

    public GameObject[] controllPieces = new GameObject[2];
    private Piece.PieceType playersType = Piece.PieceType.black;// 初期値黒プレイヤー(1P)
    private bool _isInput = false;
    private bool _isFalled = false;
    public static bool isBlack = true; // ターン処理変数
    protected float _vertical = 0.0f;
    protected float _horizontal = 0.0f;
    private int _frameCount = 0;
    private float previousTime = 0.0f;
    public int rotationNum = 0; //回転数

    private Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,  0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0,  0, -1),
        new Vector3(1,  0, 0)
    };

    void Update()
    {
        PlayerInput();
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        // 左移動
        if (((p1._horizontal < 0 || p1._stickHorizontal < 0 || p1._keyBoardHorizontal < 0) && isBlack) || 
            ((p2._horizontal < 0 || p2._stickHorizontal < 0 || p2._keyBoardHorizontal < 0) && !isBlack))
        {
            move.x = -1;
        }
        // 右移動
        else if (((p1._horizontal > 0 || p1._stickHorizontal > 0 || p1._keyBoardHorizontal > 0) && isBlack) || 
                ((p2._horizontal > 0  || p2._stickHorizontal > 0 || p2._keyBoardHorizontal > 0) && !isBlack))
        {
            move.x = 1;
        }
        // S入力すると一段下がる
        else if (((p1._vertical < 0  || p1._stickVertical > 0 || p1._keyBoardVertical > 0) && isBlack) || 
                ((p2._vertical < 0   || p2._stickVertical > 0 || p2._keyBoardVertical > 0) && !isBlack))
        {
            move.z = -1;
        }
        // 時間落下
        else if (Time.time - previousTime >= fallTime)
        {
            move.z = -1;

            previousTime = Time.time;
        }

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
        if (((p1._ds4L1 || p1._ds4cross || p1._keyBoardLeft) && isBlack) || 
            ((p2._ds4L1 || p2._ds4cross || p2._keyBoardLeft) && !isBlack))
            rotationNum++;
        // 右回転(=左に3回転)
        else if (((p1._ds4R1 || p1._ds4circle || p1._keyBoardRight) && isBlack) ||
                ((p2._ds4R1  || p2._ds4circle || p2._keyBoardRight) && !isBlack))
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

    protected void PlayerInput()
    {
        _frameCount++;
        _frameCount %= moveSpeed;

        if (!GameDirector.isGenerate)
        {
            _isInput = false;
            Rotate();

            if (_frameCount == 0)
            {
                Move();

                if (_isInput)
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
                        else if (piece1.pieceType == playersType)
                        {
                            priorityPiece = controllPieces[0];
                            nonPriorityPiece = controllPieces[1];
                        }
                        else
                        {
                            priorityPiece = controllPieces[1];
                            nonPriorityPiece = controllPieces[0];
                        }


                        // リバース・アニメーション処理
                        if (_map.CheckHeightOver(priorityPiece))
                            StartCoroutine(_map.CheckReverse(priorityPiece));
                        if (_map.CheckHeightOver(nonPriorityPiece))
                            StartCoroutine(_map.CheckReverse(nonPriorityPiece));

                        // ゲーム終了判定
                        if (_map.CheckMap())
                        {
                            Debug.LogError("end");
                            // ここに終了処理を書く
                        }
                        else
                        {
                            GameDirector.isGenerate = true;
                            _isFalled = false;
                        }
                        
                        // ターン変更
                        PlayerTurn();
                    }
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーターン処理関数
    /// </summary>
    private void PlayerTurn()
    {
        // 黒 -> 白プレイヤー
        if (isBlack)
        {
            isBlack = false;
            playersType = Piece.PieceType.white;
            Debug.Log("白プレイヤー(2P)");
        }
        // 白 -> 黒プレイヤー
        else if (!isBlack)
        {
            isBlack = true;
            playersType = Piece.PieceType.black;

            Debug.Log("黒プレイヤー(1P)");
        }
        
        // 自由落下速度初期化
        fallTime = 1.0f;
    }
}