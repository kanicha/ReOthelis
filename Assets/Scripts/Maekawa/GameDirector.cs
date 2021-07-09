using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField, Header("基本スコア")]
    public int point = 0;
    [SerializeField, Header("接地中に配置を確定するまでの時間")]
    private float _marginTime = 0;
    [SerializeField, Header("ミノの初期位置")]
    private Vector3 _DEFAULT_POSITION = Vector3.zero;
    [SerializeField]
    PieceGenerator _generator = null;
    [SerializeField]
    private Map _map = null;
    [SerializeField]
    private Player_1 _player1 = null;
    [SerializeField]
    private Player_2 _player2 = null;

    private int _turnCount = 0;
    private float _timeCount = 0;
    private GameObject[] _activePieces = new GameObject[2];
    //private GameObject[] _disActivePieces = new GameObject[2];
    public static bool isConfirmed = false;
    private bool isFalled = false;
    public static bool isGameEnd = false;

    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
        isGameEnd = false;
        isConfirmed = false;
        _player1.isMyTurn = false;
        _player2.isMyTurn = false;
        Player_1.score = 0;
        Player_2.score = 0;

        // 最初は2セット生成
        _activePieces[0] = _generator.Generate(_DEFAULT_POSITION);
        _activePieces[1] = _generator.Generate(_DEFAULT_POSITION + new Vector3(0, 0, 1));
        Debug.Log(_activePieces[0]);
        ChangeTurn();
    }

    void Update()
    {
        // 接地時にカウント
        if (_map.CheckLanding(_activePieces[0].transform.position) || _map.CheckLanding(_activePieces[1].transform.position))
        {
            _timeCount += Time.deltaTime;
            if (_timeCount > _marginTime)
                isConfirmed = true;
        }
        else
            _timeCount = 0;

        // プレイヤーの操作が確定したら
        if(isConfirmed)
        {
            if (_map.CheckLanding(_activePieces[0].transform.position))
            {
                _map.FallPiece(_activePieces[0]);
                _map.FallPiece(_activePieces[1]);
            }
            else
            {
                // 回転側が接地したら回転側から落とす
                _map.FallPiece(_activePieces[1]);
                _map.FallPiece(_activePieces[0]);
            }
            isFalled = true;
        }

        if (isFalled)
        {
            SoundManager.Instance.PlaySE(3);

            CheckPriority();
            _map.TagClear();

            // リバース・アニメーション処理
            for (int i = 0; i < _activePieces.Length; i++)
            {
                if (_map.CheckHeightOver(_activePieces[i]))
                    StartCoroutine(_map.CheckReverse(_activePieces[i]));
            }

            isConfirmed = false;
            isFalled = false;
            _player1.isMyTurn = false;
            _player2.isMyTurn = false;

            // ゲーム終了判定
            if (_map.CheckMap())
            {
                isGameEnd = true;
            }
            else
            {
                _activePieces[0] = _generator.Generate(_DEFAULT_POSITION);
                _activePieces[1] = _generator.Generate(_DEFAULT_POSITION + new Vector3(0, 0, 1));
                ChangeTurn();
            }
        }
    }

    private void CheckPriority()
    {
        // ターンプレイヤーの色を判定
        Piece.PieceType playersType;

        if (_player1.isMyTurn)
            playersType = Piece.PieceType.black;
        else
            playersType = Piece.PieceType.white;

        _map.turnPlayerColor = playersType;

        // どちらのコマからひっくり返すか判定
        GameObject tempPiece = _activePieces[0];
        Piece piece1 = _activePieces[0].GetComponent<Piece>();
        Piece piece2 = _activePieces[1].GetComponent<Piece>();

        // [色を比較]  どちらも自分の色 or どちらも相手の色ならポジションで判断する
        if ((piece1.pieceType == playersType && piece2.pieceType == playersType) || (piece1.pieceType != playersType && piece2.pieceType != playersType))
        {
            // [0]が上ならソート
            if ((int)_activePieces[0].transform.position.z > (int)_activePieces[1].transform.position.z)
            {
                _activePieces[0] = _activePieces[1];
                _activePieces[1] = tempPiece;
            }
            // [0]が右ならソート
            else if (_activePieces[0].transform.position.x > _activePieces[1].transform.position.x)
            {
                _activePieces[0] = _activePieces[1];
                _activePieces[1] = tempPiece;
            }
        }
        else if (piece2.pieceType == playersType)
        {
            _activePieces[0] = _activePieces[1];
            _activePieces[1] = tempPiece;
        }
    }

    private void ChangeTurn()
    {
        _turnCount++;

        _player1.charactorImage.color = new Color(1, 1, 1);
        _player2.charactorImage.color = new Color(1, 1, 1);

        // 黒ターン
        if (_turnCount % 2 == 1)
        {
            _player1.rotationNum = 0;
            _player1.controllPiece1 = _activePieces[0];
            _player1.controllPiece2 = _activePieces[1];
            _player1.isMyTurn = true;
            _player2.charactorImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else// 白ターン
        {
            _player2.rotationNum = 0;
            _player2.controllPiece1 = _activePieces[0];
            _player2.controllPiece2 = _activePieces[1];
            _player2.isMyTurn = true;
            _player1.charactorImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
}