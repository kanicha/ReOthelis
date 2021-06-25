using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material[] _material;
    private int i;
    [SerializeField]
    GameObject black;
    [SerializeField]
    GameObject white;

    // Use this for initialization
    void Start()
    {
        Init();
        i = 0;
    }

    public void Init()
    {
        black.GetComponent<Renderer>().sharedMaterial = _material[0];
        white.GetComponent<Renderer>().sharedMaterial = _material[1];
    }
}