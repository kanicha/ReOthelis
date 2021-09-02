using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameChara2P : MonoBehaviour
{
    [SerializeField] private Image charactorImage2P;
    [SerializeField] private Image charactorSkillImage2P;
    [SerializeField] private Sprite[] charactorImageArray2P;
    [SerializeField] private Sprite[] charactorSkillImageArray2P;
    
    // Start is called before the first frame update
    void Start()
    {
        charactorImage2P.sprite = charactorImageArray2P[(int) CharaImageMoved2P.charaType2P];
        charactorSkillImage2P.sprite = charactorSkillImageArray2P[(int)CharaImageMoved2P.charaType2P];
    }
}