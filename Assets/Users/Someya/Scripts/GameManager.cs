using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int choiseChar_1P;
    private int choiseChar_2P;

    public int ChoiseChar_1P
    {
        set
        {
            choiseChar_1P = Mathf.Clamp(value, 0, 3);
        }
        get
        {
            return choiseChar_1P;
        }
    }
    public int ChoiseChar_2P
    {
        set
        {
            choiseChar_2P = Mathf.Clamp(value, 0, 3);
        }
        get
        {
            return choiseChar_2P;
        }
    }
    public static bool order;
    protected override void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
