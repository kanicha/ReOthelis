using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGaugeControl : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _gauge = null;

    private const int _MAX_REVERSE_COUNT = 20;
    private Vector2 _defaultSize = Vector2.zero;

    private void Start()
    {
        _defaultSize = _gauge.size;
        DrawGauge(0);
    }

    public void DrawGauge(int reverseCount)
    {
        float ratio = (float)reverseCount / _MAX_REVERSE_COUNT;
        _gauge.size = new Vector2(_defaultSize.x, _defaultSize.y * ratio);
    }
}
