using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private Image _leftCharacter;
    [SerializeField] private Image _rightCharacter;
    [SerializeField] private Sprite[] _leftCharacterImage;
    [SerializeField] private Sprite[] _rightCharacterImage;

    // Start is called before the first frame update
    void Start()
    {
        _rightCharacter.color = new Color(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (TextDisplay._click == true)
        {
            _leftCharacter.sprite = _leftCharacterImage[TextDisplay._textNum+1];
            _rightCharacter.sprite = _rightCharacterImage[TextDisplay._textNum+1];
            if (_rightCharacterImage[TextDisplay._textNum+1] == null)
            {
                _rightCharacter.color = new Color(255,255,255,0);
            }
            else
            {
                _rightCharacter.color = new Color(255,255,255,1);
            }
        }
    }
}