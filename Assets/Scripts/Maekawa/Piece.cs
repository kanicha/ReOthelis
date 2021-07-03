using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private float marginTime = 0;
    //private float landingTimeCount = 0;
    [SerializeField]
    private Renderer[] _renderer = new Renderer[2];
    private Animator _anim = null;
    public GameObject pairPiece = null;
    private bool isLanding = false;
    public bool isFalled;

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

    private void Update()
    {
        if(!isFalled)
        {
            CheckLanding();
        }
    }

    private void CheckLanding()
    {
        int x = (int)transform.position.x;
        int z = (int)transform.position.z * -1;

        // 1���̃}�X���󂢂ĂȂ����
        if (Map.Instance._map[z + 1, x] != Map.empty)
        {
            isLanding = true;
            // director�ɒ��n�������Ƃ�`����
        }
        else
            isLanding = false;
    }

    public void FallPiece()
    {
        string type;
        switch(pieceType)
        {
            case PieceType.black:
                type = Map.black;
                break;
            case PieceType.white:
                type = Map.white;
                break;
            default:
                type = Map.empty;
                break;
        }

        int x = (int)transform.position.x;
        int z = (int)transform.position.z * -1;// z�̓}�C�i�X�����ɐi�ނ̂ŕ����𔽓]������

        int i = 0;
        int dz = 0;

        while (true)
        {
            i++;
            dz = z + i;// i�̕��������̍��W�𒲂ׂ�

            // �ݒu�����}�X����i���̃}�X���󔒂Ȃ牺�ɗ��Ƃ�
            if (Map.Instance._map[dz, x] == Map.empty)
                transform.position = new Vector3(x, 0, dz * -1);// ���]������y���}�C�i�X�ɖ߂�
            else
            {
                dz--;
                break;
            }
            // ������󔒈ȊO�ɓ�����܂ŌJ��Ԃ�
        }

        Map.Instance.InputMap(gameObject, type);
    }

    public void Reverse()
    {
        if(pieceType == PieceType.black)
        {
            pieceType = PieceType.white;
            this.name = "white";
        }
        else
        {
            pieceType = PieceType.black;
            this.name = "black";
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