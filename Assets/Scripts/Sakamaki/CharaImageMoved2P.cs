/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaImageMoved2P : MonoBehaviour
{
    [SerializeField] private Image charactorImage2P;
    [SerializeField] private Sprite[] charactorImageArray2P;
    [SerializeField] private GameObject[] charactorButtonWhite2P;
    private int _frameCount2P = 0;
    private int _moveSpeed2P = 10;
    private int _back2P = 0;
    private int _prev2P = 0;

    public enum CharaType2P
    {
        Cow,
        Mouse,
        Rabbit,
        Tiger
    }

    public CharaType2P charaType2P = CharaType2P.Cow;

    // Start is called before the first frame update
    void Start()
    {
        charactorImage2P.sprite = charactorImageArray2P[0];
        charactorButtonWhite2P[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Player2CharaMoved();
    }


    /// <summary>
    /// 2P‰æ‘œˆ—ŠÖ”
    /// </summary>
    void Player2CharaMoved()
    {
        _frameCount2P++;
        _frameCount2P %= _moveSpeed2P;

        if (_frameCount2P == 0)
        {
            // “ü—Í•”•ª
            if (p2._horizontal < 0 || Input.GetKeyDown(KeyCode.J))
                _charactorNum2P--;
            else if (p2._horizontal > 0 || Input.GetKeyDown(KeyCode.L))
                _charactorNum2P++;
        }

        // prev ‚Æ result •Ï”‚Ì’†g(intŒ^)‚ªˆá‚Á‚½ê‡•`‰æˆ—
        if (_prev2P != _charactorNum2P)
        {
            _prev2P = _charactorNum2P;

            if (_charactorNum2P < 0)
            {
                _charactorNum2P = charactorImageArray1P.Length - 1;
            }
            else if (_charactorNum2P >= charactorImageArray1P.Length)
            {
                _charactorNum2P = 0;
            }

            charactorImage2P.sprite = charactorImageArray2P[_charactorNum2P];
            charactorButtonWhite2P[_charactorNum2P].SetActive(true);

            _back2P = _charactorNum2P;
        }

        // Active‚µ‚½ƒ{ƒ^ƒ“false‚É‚·‚éˆ—
        if (_back2P >= 1)
        {
            _back2P--;
            charactorButtonWhite2P[_back2P].SetActive(false);
        }
        else if (_charactorNum2P <= _back2P)
        {
            charactorButtonWhite2P[3].SetActive(false);
        }
        else if (_charactorNum2P >= _back2P)
        {
            charactorButtonWhite2P[0].SetActive(false);
        }
    }
}
*/