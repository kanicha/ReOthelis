using UnityEngine;

public class ParticalController : SingletonMonoBehaviour<ParticalController>
{
    // パーティクルの再生時間 < リバースアニメーションの実行インターバル なら1つでいい
    private const int _SIZE = 3;
    private GameObject _root = null;
    private ParticleSystem[] _particles = new ParticleSystem[_SIZE];
    private int _idx = 0;

    private void Start()
    {
        _root = transform.gameObject;
        for (int i = 0; i < _SIZE; i++)
            _particles[i] = _root.transform.GetChild(i).GetComponent<ParticleSystem>();
    }

    public void PlayParticle(Vector3 pos)
    {
        _particles[_idx].transform.position = pos;
        _particles[_idx].Play();
        //
        _idx++;
        if (_SIZE <= _idx)
            _idx = 0;
    }
}
