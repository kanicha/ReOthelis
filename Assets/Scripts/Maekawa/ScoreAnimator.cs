using UnityEngine;
using UnityEngine.UI;

public class ScoreAnimator : SingletonMonoBehaviour<ScoreAnimator>
{
    [SerializeField]
    private float _moveDuration = 1 / 60 * 50;// 1/60fpsÇ…Ç®ÇØÇÈ50fÇ©ÇØÇƒè„Ç…à⁄ìÆ
    [SerializeField]
    private float _distance = 80;
    [SerializeField]
    private Material[] _materials = new Material[4];
    private GameObject _root = null;
    private Text[] _texts = new Text[_SIZE];
    private Animator _animator = null;
    private const float _PARENT_WIDTH = 720;
    private const float _PARENT_HEIGHT = 900;
    private const float _CELL_NUM_X = 8;
    private const float _CELL_NUM_Y = 10;
    private const int _SIZE = 5;
    private int _idx = 0;
    private float[] _times = new float[_SIZE];

    private void Start()
    {
        _root = transform.gameObject;
        for (int i = 0; i < _SIZE; i++)
            _texts[i] = _root.transform.GetChild(i).gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        for (int i = 0; i < _SIZE; i++)
        {
            _times[i] -= Time.deltaTime;
            if (_times[i] < 0)
                continue;

            float moveY = _distance * Time.deltaTime * _moveDuration;
            _texts[i].rectTransform.localPosition += Vector3.up * moveY;
        }
    }

    public void OnAddScore(CharaImageMoved.CharaType1P type, Vector3 pos, int point)
    {
        _texts[_idx].material = _materials[(int)type];
        _texts[_idx].text = point.ToString();
        _texts[_idx].rectTransform.anchoredPosition = new Vector2(_PARENT_WIDTH / _CELL_NUM_X * (pos.x - 1), _PARENT_HEIGHT / _CELL_NUM_Y * pos.z);
        _animator = _texts[_idx].GetComponent<Animator>();
        _animator.Play("Score", 0, 0);
        //
        _times[_idx] = _moveDuration;
        //
        _idx++;
        if (_SIZE <= _idx)
            _idx = 0;
    }
}
