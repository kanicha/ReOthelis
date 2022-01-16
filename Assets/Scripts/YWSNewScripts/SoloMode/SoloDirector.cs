using UnityEngine;
using UnityEngine.UI;

public class SoloDirector : SingletonMonoBehaviour<GameDirector>
{
    [SerializeField, Header("��{�X�R�A")]
    public int point = 0;
    [SerializeField, Header("�ڒn���ɔz�u���m�肷��܂ł̎���")]
    private float _marginTime = 0;
    [SerializeField, Header("���O�ɑ���ł��鎞��")]
    private float _preActiveTime = 0;
    [SerializeField, Header("�~�m�̏����ʒu")]
    public Vector3 _DEFAULT_POSITION = Vector3.zero;
    [SerializeField]
    PiecePatternGeneretor _generator = null;
    [SerializeField]
    private Player_1 _player1 = null;
    [SerializeField]
    private Player_2 _player2 = null;
    [SerializeField, Header("�G�t�F�N�g�R���g���[���[")] 
    private EffectController _effectController = null;
    [SerializeField]
    private AI_DataBase _ai = null;

    private int _turnCount = 0;
    private float _timeCount = 0;
    private bool _isDown = true;
    public GameObject[] _activePieces = new GameObject[2];
    public float intervalTime = 0;
    public bool _isLanding = false;
    public bool _isSkillBlack = false;
    public bool _isSkillWhite = false;
    public GameState gameState = GameState.none;
    public GameState nextStateCue = GameState.none;

    public enum GameState
    {
        none,
        preActive,
        active,
        confirmed,
        falled,
        interval,
        reversed,
        idle,
        end,
        ended,
    }

    void Start()
    {
        SoundManager.Instance.PlayBGM(0);

        _player1.isMyTurn = false;
        _player2.isMyTurn = false;

        // �ŏ���2�Z�b�g����
        PieceSet();
        ChangeTurn();
    }

    void Update()
    {
        Map.Instance.CheckMap();

        switch (gameState)
        {
            case GameState.preActive:
                _effectController.FallPieceHighLight(true);

                _isLanding = false;
                _isDown = true;
                _timeCount += Time.deltaTime;

                // �ҋ@���Ԃ𒴂�����X�e�[�g�������߂�(���������̏�����PieceMove()�ŊǗ�)
                if (_timeCount > _preActiveTime)
                {
                    // ���Ԍo�߂ɂ��R�}���������
                    _activePieces[0].transform.position += new Vector3(0, 0, -1);
                    _activePieces[1].transform.position += new Vector3(0, 0, -1);

                    // �������琄��
                    intervalTime = 0;
                    nextStateCue = GameState.active;
                    gameState = GameState.interval;
                }
                break;

            case GameState.active:
                _effectController.FallPieceHighLight(true);

                if (ModeSelect._selectCount == 0 && _player2.isMyTurn == true)
                {
                    _ai.MovePiece();
                }
                if (Map.Instance.CheckLanding(_activePieces[0].transform.position) || Map.Instance.CheckLanding(_activePieces[1].transform.position))
                {
                    // �ڒn���ɃJ�E���g
                    _timeCount += Time.deltaTime;
                    if (_timeCount > _marginTime)
                    {
                        _timeCount = 0;
                        gameState = GameState.confirmed;
                    }
                }
                else
                    _timeCount = 0;
                break;

            case GameState.confirmed:
                _effectController.FallPieceHighLight(false);
                if (_activePieces[0].transform.position.z > _activePieces[1].transform.position.z)
                {
                    // �����̃R�}���C���f�b�N�X0�ɂȂ�悤�\�[�g
                    GameObject tempPiece = _activePieces[0];
                    _activePieces[0] = _activePieces[1];
                    _activePieces[1] = tempPiece;
                }

                Map.Instance.FallPiece(_activePieces[0]);
                Map.Instance.FallPiece(_activePieces[1]);
                _isLanding = true;
                gameState = GameState.falled;
                break;

            case GameState.falled:
                SoundManager.Instance.PlaySE(3);
                CheckPriority();
                Map.Instance.TagClear();

                gameState = GameState.idle;
                // ���o�[�X�E�A�j���[�V��������
                for (int i = 0; i < _activePieces.Length; i++)
                {
                    if (Map.Instance.CheckHeightOver(_activePieces[i], false))
                        StartCoroutine(Map.Instance.CheckReverse(_activePieces[i], false));
                }

                // �X�L���t���O������
                _isSkillBlack = false;
                _isSkillWhite = false;
                break;

            case GameState.interval:// �����X�L���A�łŃo�O���o��̂Ŏ��Ԃ����(���}���u)
                _timeCount += Time.deltaTime;
                if (_timeCount > intervalTime)
                {
                    gameState = nextStateCue;
                    _timeCount = 0;
                }
                break;

            case GameState.reversed:
                // �Q�[���I������
                if (Map.Instance.CheckEnd())
                    gameState = GameState.end;
                else
                {
                    PieceSet();
                    ChangeTurn();
                }
                break;

            case GameState.end:
                if (_player1.reverseScore > _player2.reverseScore)
                    Debug.Log("<color=red>1P�̏���</color>");
                else if (_player1.reverseScore == _player2.reverseScore)
                    Debug.Log("<color=orange>��������</color>");
                else
                    Debug.Log("<color=blue>2P�̏���</color>");
                SoundManager.Instance.StopBGM();
                gameState = GameState.ended;
                break;

            default:
                break;
        }
    }

    private void CheckPriority()
    {
        // �^�[���v���C���[�̐F�𔻒�
        Piece.PieceType playersType;

        if (_player1.isMyTurn)
            playersType = Piece.PieceType.black;
        else
            playersType = Piece.PieceType.white;

        Map.Instance.turnPlayerColor = playersType;

        // �ǂ���̃R�}����Ђ�����Ԃ�������
        GameObject tempPiece = _activePieces[0];
        Piece piece1 = _activePieces[0].GetComponent<Piece>();
        Piece piece2 = _activePieces[1].GetComponent<Piece>();

        // [�F���r]  �ǂ���������̐F or �ǂ��������̐F�Ȃ�|�W�V�����Ŕ��f����
        if ((piece1.pieceType == playersType && piece2.pieceType == playersType) || (piece1.pieceType != playersType && piece2.pieceType != playersType))
        {
            // [0]����Ȃ�\�[�g
            if ((int)_activePieces[0].transform.position.z > (int)_activePieces[1].transform.position.z)
            {
                _activePieces[0] = _activePieces[1];
                _activePieces[1] = tempPiece;
            }
            // [0]���E�Ȃ�\�[�g
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

        _player1.isMyTurn = false;
        _player2.isMyTurn = false;

        // ���^�[��
        if (_turnCount % 2 == 1)
        {
            _player1.rotationNum = 0;
            _player1.controllPiece1 = _activePieces[0];
            _player1.controllPiece2 = _activePieces[1];
            _player1.isMyTurn = true;
        }
        else// ���^�[��
        {
            _player2.rotationNum = 0;
            _player2.controllPiece1 = _activePieces[0];
            _player2.controllPiece2 = _activePieces[1];
            _player2.isMyTurn = true;
            _ai.MapPrepare();
            _ai.CheckVertical();
            _ai.PatternClassification();
            _ai.CheckMap();
            _ai.PatternChoice();
        }
        gameState = GameState.preActive;
    }

    private void PieceSet()
    {
        // �����ʒu��1�}�X�����󂢂Ă���ΐ���
        Vector3 generatePos = _DEFAULT_POSITION + Vector3.back;
        int x = 0;
        while (true)
        {
            Vector3 checkPos = generatePos + new Vector3(x, 0);
            if (Map.Instance.CheckWall(checkPos))
            {
                _generator.Generate(checkPos + Vector3.forward);
                break;
            }
            else
            {
                checkPos = generatePos + new Vector3(x * -1, 0);
                if (Map.Instance.CheckWall(checkPos))
                {
                    _generator.Generate(checkPos + Vector3.forward);
                    break;
                }
            }
            x++;
            if (x > 4)
                Debug.LogError("�����ł���}�X������܂���");
        }
    }

    public void AddScore(bool isBlack, int point)
    {
        if (isBlack)
        {
            _player1.reverseScore += point;
        }
        else
        {
            _player2.reverseScore += point;
        }
    }

    public void AddPreScore(bool isBlack, int point)
    {
        if (isBlack)
        {
            _player1.preScore += point;
        }
        else
        {
            _player2.preScore += point;
        }
    }

    public void AddReversedCount(bool isBlack)
    {
        if (isBlack)
            _player1.reversedCount++;
        else
            _player2.reversedCount++;
    }

    public void AddPieceCount(int blackCount, int whiteCount)
    {
        _player1.myPieceCount = blackCount;
        _player2.myPieceCount = whiteCount;
    }
}