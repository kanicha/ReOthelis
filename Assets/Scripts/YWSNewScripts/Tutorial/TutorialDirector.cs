using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : SingletonMonoBehaviour<TutorialDirector>
{
    [SerializeField, Header("基本スコア")]
    public int point = 0;
    [SerializeField, Header("接地中に配置を確定するまでの時間")]
    private float _marginTime = 0;
    [SerializeField, Header("事前に操作できる時間")]
    private float _preActiveTime = 0;
    [SerializeField, Header("ミノの初期位置")]
    public Vector3 _DEFAULT_POSITION = Vector3.zero;
    [SerializeField]
    PiecePatternGeneretorForT _generator = null;
    [SerializeField]
    private Player_T1 _player1 = null;
    [SerializeField, Header("エフェクトコントローラー")] private EffectControllerForT _effectController = null;

    private float _timeCount = 0;
    private bool _isDown = true;
    public GameObject[] _activePieces = new GameObject[2];
    public float intervalTime = 0;
    public bool _isLanding = false;
    public bool _isSkillBlack = false;
    public GameState gameState = GameState.none;
    public GameState nextStateCue = GameState.none;

    private GameSceneManager _gameSceneManager;
    public bool isFadeIn = false;
    public bool isFadeOut = false;
    public static bool isTutorialEnd = false;
    public Image blackOutImage; //画面を暗くする用のイメージ
    [SerializeField] private Text explanText;
    private string _showText;
    [SerializeField] private Image explanImage;
    [SerializeField] private Sprite[] showImage;
    public static bool ReverseFin = false;
    public bool skillUsed = false;

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

    public TutorialPhase tutorialPhase = TutorialPhase.none;
    public enum TutorialPhase
    {
        none,
        Intro,
        SpinLeft,
        SpinRight,
        MoveLeft,
        MoveRight,
        Reverse,
        SkillPanel,
        SkillActive,
        End,
    }

    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _player1.isMyTurn = false;
        blackOutImage.color = new Color(0,0,0,0.5f);
        isFadeIn = true;
        isFadeOut = false;
        isTutorialEnd = false;

        // 最初は2セット生成
        PieceSet();
        gameState = GameState.preActive;
        ChangeTurn();
        tutorialPhase = TutorialPhase.Intro;
    }

    void Update()
    {
        TutorialMap.Instance.CheckMap();

        switch (gameState)
        {
            case GameState.preActive:
                _effectController.FallPieceHighLight(true);
                
                _isLanding = false;
                _isDown = true;
                _timeCount += Time.deltaTime;

                switch(tutorialPhase)
                {
                    case TutorialPhase.Intro:
                        if (_gameSceneManager.IsChanged == true)
                        {
                            blackOutImage.color = new Color(0, 0, 0, 0.5f);
                            isFadeIn = true;
                            explanText.text = "〇ボタンを押して次に進む。";
                            //explanImage.sprite = showImage[0];
                        }
                        break;

                    case TutorialPhase.MoveLeft:
                        explanText.text = "左キーでコマを左端まで移動してください。";
                        //explanImage.sprite = showImage[1];
                        break;

                    case TutorialPhase.MoveRight:
                        explanText.text = "右キーでコマを右端まで移動してください。";
                        //explanImage.sprite = showImage[2];
                        break;

                    case TutorialPhase.SpinLeft:
                        explanText.text = "L1キーでコマを回転してください。";
                        //explanImage.sprite = showImage[3];
                        break;

                    case TutorialPhase.SpinRight:
                        explanText.text = "R1キーでコマを回転してください。";
                        //explanImage.sprite = showImage[4];
                        break;

                    case TutorialPhase.SkillPanel:
                        explanText.text = "optionボタンを押してスキル効果の詳細を表示する。";
                        //explanImage.sprite = showImage[];
                        break;

                    case TutorialPhase.SkillActive:
                        explanText.text = "□ボタンを押して必殺技を発動する。";
                        //explanImage.sprite = showImage[];
                        if (skillUsed == true)
                        {
                            explanText.text = "必殺技を使用しました。\nこれにてチュートリアルを終了します。\n〇ボタンでタイトル画面に戻ります。";
                            tutorialPhase = TutorialPhase.End;
                        }
                        break;

                    default:
                        break;
                }

                // 待機時間を超えたらステートをすすめる(自動落下の処理はPieceMove()で管理)
                if (tutorialPhase == TutorialPhase.Reverse && _timeCount > _preActiveTime)
                {
                    // 時間経過によりコマを一個下げる
                    _activePieces[0].transform.position += new Vector3(0, 0, -1);
                    _activePieces[1].transform.position += new Vector3(0, 0, -1);
                    
                    explanText.text = "コマは自動で落下し、一番下で着地したら、\nオセロみたいに同じ色で挟んだコマをひっくり返します。";
                    //explanImage.sprite = showImage[5];

                    // さげたら推移
                    intervalTime = 0;
                    nextStateCue = GameState.active;
                    gameState = GameState.interval;
                }
                break;

            case GameState.active:
                _effectController.FallPieceHighLight(true);
                
                if (TutorialMap.Instance.CheckLanding(_activePieces[0].transform.position) || TutorialMap.Instance.CheckLanding(_activePieces[1].transform.position))
                {
                    // 接地時にカウント
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
                    // 下側のコマがインデックス0になるようソート
                    GameObject tempPiece = _activePieces[0];
                    _activePieces[0] = _activePieces[1];
                    _activePieces[1] = tempPiece;
                }
                
                TutorialMap.Instance.FallPiece(_activePieces[0]);
                TutorialMap.Instance.FallPiece(_activePieces[1]);
                _isLanding = true;
                gameState = GameState.falled;
                break;

            case GameState.falled:
                SoundManager.Instance.PlaySE(3);
                CheckPriority();
                TutorialMap.Instance.TagClear();

                gameState = GameState.idle;
                // リバース・アニメーション処理
                for (int i = 0; i < _activePieces.Length; i++)
                {
                    if(TutorialMap.Instance.CheckHeightOver(_activePieces[i],false))
                        StartCoroutine(TutorialMap.Instance.CheckReverse(_activePieces[i],false));
                }
                
                // スキルフラグ初期化
                _isSkillBlack = false;
                break;

            case GameState.interval:// 強引スキル連打でバグが出るので時間を取る(応急処置)
                _timeCount += Time.deltaTime;
                if (_timeCount > intervalTime)
                {
                    gameState = nextStateCue;
                    _timeCount = 0;
                }                
                break;

            case GameState.reversed:
                // ゲーム終了判定
                if (TutorialMap.Instance.CheckEnd())
                    gameState = GameState.end;
                else
                {
                    tutorialPhase = TutorialPhase.Intro;
                    ReverseFin = true;
                    PieceSet();
                    gameState = GameState.preActive;
                    ChangeTurn();
                }
                break;

            case GameState.end:
                SoundManager.Instance.StopBGM();
                gameState = GameState.ended;
                break;

            default:
                break;
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

        TutorialMap.Instance.turnPlayerColor = playersType;

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
        _player1.rotationNum = 0;
        _player1.controllPiece1 = _activePieces[0];
        _player1.controllPiece2 = _activePieces[1];
        _player1.isMyTurn = true;
        gameState = GameState.preActive;
    }

    private void PieceSet()
    {
        // 生成位置の1マス下が空いていれば生成
        Vector3 generatePos = _DEFAULT_POSITION + Vector3.back;
        int x = 0;
        while(true)
        {
            Vector3 checkPos = generatePos + new Vector3(x, 0);
            if (TutorialMap.Instance.CheckWall(checkPos))
            {
                _generator.Generate(checkPos + Vector3.forward);
                break;
            }
            else
            {
                checkPos = generatePos + new Vector3(x * -1, 0);
                if (TutorialMap.Instance.CheckWall(checkPos))
                {
                    _generator.Generate(checkPos + Vector3.forward);
                    break;
                }
            }
            x++;
            if (x > 4)
                Debug.LogError("生成できるマスがありません");
        }
    }

    public void AddScore(bool isBlack, int point)
    {
        if(isBlack)
        {
            _player1.reverseScore += point;
        }
    }

    public void AddPreScore(bool isBlack, int point)
    {
        if (isBlack)
        {
            _player1.preScore += point;
        }
    }

    public void AddReversedCount(bool isBlack)
    {
        if(isBlack)
            _player1.reversedCount++;
    }

    public void AddPieceCount(int blackCount, int whiteCount)
    {
        _player1.myPieceCount = blackCount;
    }
}