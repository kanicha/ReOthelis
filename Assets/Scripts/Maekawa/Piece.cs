using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderer = new Renderer[2];
    [SerializeField] GameObject _particalObj = default(GameObject);
    public Material[] _material;
    private Animator _anim = null;

    public enum PieceType
    {
        none,
        black,
        white,
        fixityBlack,
        fixityWhite
    }

    public PieceType pieceType = PieceType.none;
    public int rotationNum = 0;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        Init();
    }

    public void Init()
    {
        if (ModeSelect._selectCount == 2)
        {
            _renderer[0].GetComponent<Renderer>().sharedMaterial = _material[2];
            _renderer[1].GetComponent<Renderer>().sharedMaterial = _material[6];
        }
        else
        {
            _renderer[0].GetComponent<Renderer>().sharedMaterial = _material[(int)CharaImageMoved.charaType1P];
            _renderer[1].GetComponent<Renderer>().sharedMaterial = _material[(int)CharaImageMoved2P.charaType2P + 4];
        }
    }

    public void Reverse()
    {
        if (pieceType == PieceType.black)
        {
            pieceType = PieceType.white;
            this.name = "white";
        }
        else if (pieceType == PieceType.white)
        {
            pieceType = PieceType.black;
            this.name = "black";
        }

        _anim.SetTrigger("Reverse");
        SoundManager.Instance.PlaySE(4);
    }

    public void SkillReverse(bool se)
    {
        if (pieceType == PieceType.black)
        {
            pieceType = PieceType.white;
            this.name = "white";
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            pieceType = PieceType.black;
            this.name = "black";
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (se)
            SoundManager.Instance.PlaySE(4);
        else
            return;
    }

    public void ChangeColor(bool isActive)
    {
        Color setColor;
        if (isActive)
            setColor = new Color(1, 1, 1);
        else
            setColor = new Color(0.5f, 0.5f, 0.5f);

        for (int i = 0; i < _renderer.Length; i++)
            _renderer[i].material.color = setColor;
    }

    public void ChangeIsFixity()
    {
        switch (pieceType)
        {
            case PieceType.black:
                pieceType = PieceType.fixityBlack;
                break;
            case PieceType.white:
                pieceType = PieceType.fixityWhite;
                break;
            case PieceType.fixityBlack:
                pieceType = PieceType.black;
                break;
            case PieceType.fixityWhite:
                pieceType = PieceType.white;
                break;
        }

        if (pieceType == PieceType.fixityBlack)
        {
            _particalObj.SetActive(true);
        }
        else if (pieceType == PieceType.fixityWhite)
        {
            // 白コマはエフェクトの座標が反転してしまうため y の値を増加させる
            Vector3 pos = _particalObj.transform.position;
            pos.y = 1f;
            _particalObj.transform.position = pos;
            
            _particalObj.SetActive(true);
        }
        else
        {
            // 解除
            _particalObj.SetActive(false);
        }
    }
}