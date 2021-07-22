using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject piecePrefab = null;
    private int _range = 0;
    private int _whiteCount, _blackCount, _jokerCount = 0;
    private const int _WHITE_COUNT_MAX = 15;
    private const int _BLACK_COUNT_MAX = 15;
    private const int _JOKER_COUNT_MAX = 0;// ��Ujoker�Ȃ�

    private void Start()
    {
        _range = _WHITE_COUNT_MAX + _BLACK_COUNT_MAX;//+ _JOKER_COUNT_MAX;
    }

    public GameObject Generate(Vector3 GeneratePos)
    {
        // �R�}�^�C�v
        int type;

        // �ꐔ������{�b�N�X�K�`���`���ŃW���[�J�[�𒊑I
        int num = Random.Range(0, _range);
        // 0 ~ 99�ō��Ɣ����I�敪
        int num2 = Random.Range(0, 1000);

        // �������ƂɌ�������_renge�ϐ���0 & ��������ɒB���Ă��Ȃ���΃W���[�J�[
        if (num == 0 && _jokerCount < _JOKER_COUNT_MAX)
        {
            type = 3;
            _jokerCount++;
        }
        // �]�肪0 & ���̐�����������ɒB���Ă��Ȃ��@or ���̐�����������ɒB���Ă���Ȃ獕
        else if ((num2 % 2 == 0 && _blackCount < _BLACK_COUNT_MAX) || _whiteCount >= _WHITE_COUNT_MAX)
        {
            type = 1;
            _blackCount++;
        }
        // ��L�ɊY�����Ȃ���Δ�
        else
        {
            type = 2;
            _whiteCount++;
        }

        // ��񒊑I������͈͂���߂�
        _range--;

        // �͈͂�0�ɂȂ������Ƀ��Z�b�g����
        if (_range == 0)
        {
            Debug.Log($"<color=orange>Joker : {_jokerCount} B : {_blackCount} W : {_whiteCount} </color>");
            _range = _WHITE_COUNT_MAX + _BLACK_COUNT_MAX;//+ _JOKER_COUNT_MAX;
            _jokerCount = 0;
            _blackCount = 0;
            _whiteCount = 0;
        }

        GameObject piece = Instantiate(piecePrefab);
        piece.transform.parent = root.transform;
        piece.transform.position = GeneratePos;
        Piece p = piece.GetComponent<Piece>();

        switch (type)
        {
            case 1:
                piece.name = "black";
                p.pieceType = Piece.PieceType.black;
                break;
            case 2:
                piece.name = "white";
                p.pieceType = Piece.PieceType.white;
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            //case 3:
            //    piece.name = "joker";
            //    p.pieceType = Piece.PieceType.joker;
                //break;
            default:
                break;
        }

        return piece;
    }
}

