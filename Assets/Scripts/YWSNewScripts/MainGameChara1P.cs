using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameChara1P : MonoBehaviour
{
    [SerializeField] private Image charactorImage1P;
    [SerializeField] private Sprite[] charactorImageArray1P;

    // Start is called before the first frame update
    void Start()
    {
        charactorImage1P.sprite = charactorImageArray1P[(int)CharaImageMoved.charaType1P];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}