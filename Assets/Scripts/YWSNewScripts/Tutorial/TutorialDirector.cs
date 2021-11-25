using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : Player1Base
{
    public Image blackOutImage; //画面を暗くする用のイメージ
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
        PartTwo,
        PartThree,
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
                    tutorialPhase = TutorialPhase.SpinLeft;
                }
                break;

            case TutorialPhase.SpinLeft:
                if (_DS4_L1_value || Input.GetKeyDown(KeyCode.M))
                {
                    base.PieceRotate();
                }
                break;

            case TutorialPhase.SpinRight:
            if (_DS4_R1_value || Input.GetKeyDown(KeyCode.N))
                {
                    base.PieceRotate();
                }
                break;

            case TutorialPhase.MoveLeft:
                break;

            case TutorialPhase.MoveRight:
                break;

            case TutorialPhase.PartTwo:
                break;

            case TutorialPhase.PartThree:
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
}
