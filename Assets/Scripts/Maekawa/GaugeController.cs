using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    [SerializeField, Header("ëŒè€ÉvÉåÉCÉÑÅ[")]
    private Player _player = Player.none;
    [SerializeField]
    private SpriteRenderer _gauge = null;
    [SerializeField]
    private Sprite[] _charaGauges = new Sprite[4];

    private const int _MAX_REVERSE_COUNT = 20;
    private Vector2 _defaultSize = Vector2.zero;
    private enum Player
    {
        none,
        player1,
        player2,
    }

    private void Start()
    {
        switch(_player)
        {
            case Player.player1:
                _gauge.sprite = _charaGauges[(int)CharaImageMoved.charaType1P];
                break;
            case Player.player2:
                _gauge.sprite = _charaGauges[(int)CharaImageMoved2P.charaType2P];
                break;
            default:
                break;

        }
        _defaultSize = _gauge.size;
        DrawGauge(0);
    }

    public void DrawGauge(int reverseCount)
    {
        float ratio = (float)reverseCount / _MAX_REVERSE_COUNT;
        _gauge.size = new Vector2(_defaultSize.x, _defaultSize.y * ratio);
    }
}
