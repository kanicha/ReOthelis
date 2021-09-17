using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonManager : MonoBehaviour
{
    private static bool Loaded { get; set; } = false;
    private static GameObject _commoGameObject = null;
    public static GameObject CommonGameManager { get { return _commoGameObject; } }
    [SerializeField]
    private GameObject _commonPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (Loaded) return;
        Loaded = true;
        _commoGameObject = Instantiate(_commonPrefab);
        DontDestroyOnLoad(_commoGameObject);
    }
}
