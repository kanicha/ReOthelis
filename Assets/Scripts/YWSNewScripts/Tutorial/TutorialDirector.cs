using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : Player1Base
{
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
        Skill,
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialPhase = TutorialPhase.Intro;
    }

    // Update is called once per frame
    void Update()
    {
        switch(tutorialPhase)
        {
            case TutorialPhase.Intro:
            FadeIn();
            _explanText.text = "導入パート";
            //_explanImage.sprite = _showImage[0];
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space) || _DS4_circle_value || _DS4_cross_value)
            {
                FadeOut();
                PieceSet();
                tutorialPhase = TutorialPhase.SpinLeft;
            }
            break;

            case TutorialPhase.SpinLeft:
            if (_DS4_L1_value || Input.GetKeyDown(KeyCode.M))
            {
                base.PieceRotate();
                tutorialPhase = TutorialPhase.SpinRight;
            }
            break;

            case TutorialPhase.SpinRight:
            if (_DS4_R1_value || Input.GetKeyDown(KeyCode.N))
            {
                base.PieceRotate();
                tutorialPhase = TutorialPhase.MoveLeft;
            }
            break;

            case TutorialPhase.MoveLeft:
            if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) ||
                (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            {
                Vector3 move = Vector3.zero;
                move.x = -1;

                // 移動後の座標を計算
                Vector3 movedPos = controllPiece1.transform.position + move;
                Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

                // 移動後の座標に障害物がなければ
                if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = rotMovedPos;
                }
                tutorialPhase = TutorialPhase.MoveRight;
            }
            break;

            case TutorialPhase.MoveRight:
            if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) ||
                (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            {
                Vector3 move = Vector3.zero;
                move.x = 1;

                // 移動後の座標を計算
                Vector3 movedPos = controllPiece1.transform.position + move;
                Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

                // 移動後の座標に障害物がなければ
                if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = rotMovedPos;
                }
                tutorialPhase = TutorialPhase.Reverse;
            }
            break;

            case TutorialPhase.Reverse:
                FadeIn();
                if (Map.Instance.CheckLanding(_activePieces[0].transform.position) || Map.Instance.CheckLanding(_activePieces[1].transform.position))
                {
                    // 接地時にカウント
                    _timeCount += Time.deltaTime;
                    if (_timeCount > _marginTime)
                    {
                        _timeCount = 0;
                    }
                }
                else
                    _timeCount = 0;
                FadeOut();
                break;

            case TutorialPhase.Skill:
                break;

            default:
                break;
        }
    }

    public void FadeIn()
    {
        blackOutImage.color = new Color(0,0,0,0.5f);
    }

    public void FadeOut()
    {
        blackOutImage.color = new Color(0,0,0,0);
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
