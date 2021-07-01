using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private Renderer[] _renderer = new Renderer[2];
    private Animator _anim = null;

    public enum PieceType
    {
        none,
        black,
        white,
        joker
    }

    public PieceType pieceType = PieceType.none;
    public int rotationNum = 0;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Reverse()
    {
        if(pieceType == PieceType.black)
        {
            pieceType = PieceType.white;
            this.name = "white";
            //this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            pieceType = PieceType.black;
            this.name = "black";
            //this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        _anim.SetTrigger("Reverse");
        SoundManager.Instance.PlaySE(4);
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
}