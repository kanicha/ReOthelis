using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material[] _material;
    [SerializeField]
    GameObject black;
    [SerializeField]
    GameObject white;

    // Use this for initialization
    void Start()
    {
        Init(1,0);
    }

    /*
    �I�����ꂽ�L�����N�^�[�ɍ��킹�ċ�̃}�e���A��������������B
    0 ����
    1 �l��
    2 �e��
    3 �Ս�
    4 ����
    5 �l��
    6 �e��
    7 �Ք�
    */
    public void Init(int P1,int P2)
    {
        black.GetComponent<Renderer>().sharedMaterial = _material[P1];
        white.GetComponent<Renderer>().sharedMaterial = _material[P2+4];
    }
}