using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderer = new Renderer[2];
    [SerializeField] GameObject _particalObj = default(GameObject);
    public Material[] _material;

    private Animator _anim = null;

    // コマのID
    public string _pieceId = "";

    private MyVector3 _myVector3;

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

        // 自分が生成されたときにリストに追加する
        GameDirector.Instance.pieces.Add(this);
        if (ServerManager._isConnect)
        {
            // 自分の座標が変化した時
            this.ObserveEveryValueChanged(x => x.transform.position).Where(_ => GameDirector.Instance.player1.isMyTurn)
                .Subscribe(onMoved).AddTo(this);

            this.ObserveEveryValueChanged(x => x._myVector3).Where(_ => GameDirector.Instance.player1.isMyTurn)
                .Subscribe(SendOnMoved).AddTo(this);
        }
    }

    private void Awake()
    {
        // 自身が1pの時
        if (ServerManager._isConnect && ServerManager.Instance.myPlayerNumber == ServerManager.playerNumber.onePlayer)
        {
            // IDの生成
            _pieceId = Guid.NewGuid().ToString();
        }
    }

    public void Init()
    {
        _renderer[0].GetComponent<Renderer>().sharedMaterial = _material[(int)CharaImageMoved.charaType1P];
        _renderer[1].GetComponent<Renderer>().sharedMaterial = _material[(int)CharaImageMoved2P.charaType2P + 4];
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

        if (pieceType == PieceType.fixityBlack || pieceType == PieceType.fixityWhite)
        {
            _particalObj.SetActive(true);
        }
        else
        {
            // 解除
            _particalObj.SetActive(false);
        }
    }

    /// <summary>
    /// コマが移動を行った時
    /// </summary>
    public void onMoved(Vector3 movedPos)
    {
        _myVector3.x = movedPos.x;
        _myVector3.y = movedPos.y;
        _myVector3.z = movedPos.z;
    }

    public void SendOnMoved(MyVector3 movedPos)
    {
        // コマのリクエスト生成
        PieceMoveRequest pieceMoveRequest = new PieceMoveRequest(movedPos, this.pieceType, _pieceId);

        Debug.LogWarning("コマの座標x" + pieceMoveRequest.piecePos.x);
        Debug.LogWarning("コマの座標z" + pieceMoveRequest.piecePos.z);
        Debug.LogWarning("コマの座標y" + pieceMoveRequest.piecePos.y);

        // 送信を行う
        ServerManager.Instance.SendMessage(pieceMoveRequest);
    }
}