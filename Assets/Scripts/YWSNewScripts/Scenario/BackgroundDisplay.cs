using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundDisplay : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        //_background.sprite = _backgroundImage[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
