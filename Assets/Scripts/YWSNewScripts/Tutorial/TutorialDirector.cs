using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : Player1Base
{
    private GameSceneManager _gameSceneManager;
    private bool _isFadeIn = false;
    private bool _isFadeOut = false;
    private bool _isTutorialStart = false;
    public static bool _isTutorialEnd = false;
    public Image blackOutImage; //画面を暗くする用のイメージ
    PiecePatternGeneretor _generator = null;
    public Vector3 _DEFAULT_POSITION = Vector3.zero;
    private float _marginTime = 0;
    private float _timeCount = 0;
    public GameObject[] _activePieces = new GameObject[2];
    [SerializeField] private Text _explanText;
    private string _showText;
    [SerializeField] private Image _explanImage;
    [SerializeField] private Sprite[] _showImage;
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
        SkillIntro,
        SkillPanel,
        SkillActive,
        End,
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _isFadeIn = false;
        _isFadeOut = false;
        _isTutorialEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        if (_gameSceneManager.IsChanged == true && _isTutorialStart == false)
        {
            tutorialPhase = TutorialPhase.Intro;
            _isTutorialStart = true;
        }

        switch(tutorialPhase)
        {
            case TutorialPhase.Intro:
            FadeIn();
            _explanText.text = "導入パート";
            //_explanImage.sprite = _showImage[0];
            if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space) || _DS4_cross_value || Input.GetKeyDown(KeyCode.X))
            {
                FadeOut();
                Debug.Log("Intro End");
                //PieceSet();
                tutorialPhase = TutorialPhase.SpinLeft;
            }
            break;

            case TutorialPhase.SpinLeft:
            _explanText.text = "L1キーを押してください。";
            if (_DS4_L1_value || Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("L1 Key Input");
                //base.PieceRotate();
                tutorialPhase = TutorialPhase.SpinRight;
            }
            break;

            case TutorialPhase.SpinRight:
            _explanText.text = "R1キーを押してください。";
            if (_DS4_R1_value || Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("R1 Key Input");
                //base.PieceRotate();
                tutorialPhase = TutorialPhase.MoveLeft;
            }
            break;

            case TutorialPhase.MoveLeft:
            _explanText.text = "左キーを押してください。";
            if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) ||
                (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            {
                Debug.Log("Left Key Input");
                /*Vector3 move = Vector3.zero;
                move.x = -1;

                // 移動後の座標を計算
                Vector3 movedPos = controllPiece1.transform.position + move;
                Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

                // 移動後の座標に障害物がなければ
                if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = rotMovedPos;
                }*/
                tutorialPhase = TutorialPhase.MoveRight;
            }
            break;

            case TutorialPhase.MoveRight:
            _explanText.text = "右キーを押してください。";
            if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) ||
                (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            {
                Debug.Log("Right Key Input");
                /*Vector3 move = Vector3.zero;
                move.x = 1;

                // 移動後の座標を計算
                Vector3 movedPos = controllPiece1.transform.position + move;
                Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

                // 移動後の座標に障害物がなければ
                if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = rotMovedPos;
                }*/
                tutorialPhase = TutorialPhase.Reverse;
            }
            break;

            case TutorialPhase.Reverse:
            FadeIn();
            _explanText.text = "ひっくり返しパート";
            if (_isFadeIn == true && Input.GetKeyDown(KeyCode.Space) || _isFadeIn == true && _DS4_circle_value)
            {
                FadeOut();
                tutorialPhase = TutorialPhase.SkillIntro;
            }
            /*if (Map.Instance.CheckLanding(_activePieces[0].transform.position) || Map.Instance.CheckLanding(_activePieces[1].transform.position))
            {
                // 接地時にカウント
                _timeCount += Time.deltaTime;
                if (_timeCount > _marginTime)
                {
                    _timeCount = 0;
                }
            }
            else
                _timeCount = 0;*/
            break;

            case TutorialPhase.SkillIntro:
            FadeIn();
            _explanText.text = "スキルパート";
            if (Input.GetKeyDown(KeyCode.Space) || _DS4_circle_value)
            {
                FadeOut();
                tutorialPhase = TutorialPhase.SkillPanel;
            }
            break;

            case TutorialPhase.SkillPanel:
            _explanText.text = "スキルパネルを開いてください。";
            if (Input.GetKeyDown(KeyCode.N) || _DS4_option_value)
            {
                Debug.Log("Skill Panel On");
                tutorialPhase = TutorialPhase.SkillActive;
            }
            break;

            case TutorialPhase.SkillActive:
            _explanText.text = "必殺技を使ってください。";
            if (_DS4_square_value)
            {
                Debug.Log("Skill Active");
                tutorialPhase = TutorialPhase.End;
            }
            break;

            case TutorialPhase.End:
            _explanText.text = "終了";
            _isTutorialEnd = true;
            break;

            default:
                break;
        }
    }

    public void FadeIn()
    {
        blackOutImage.color = new Color(0,0,0,0.5f);
        _isFadeIn = true;
        _isFadeOut = false;
    }

    public void FadeOut()
    {
        blackOutImage.color = new Color(0,0,0,0);
        _isFadeOut = true;
        _isFadeIn = false;
    }

    private void PieceSet()
    {
        // 生成位置の1マス下が空いていれば生成
        Vector3 generatePos = _DEFAULT_POSITION + Vector3.back;
        int x = 0;
        while(true)
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
                Debug.LogError("生成できるマスがありません");
        }
    }
}
