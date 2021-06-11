using UnityEngine;

public class MinoController : MonoBehaviour
{
    [SerializeField]
    Map myMap = null;
    public static GameObject[] controllPieces = new GameObject[2];

    private float previousTime = 0f;
    [SerializeField]
    float fallTime = 1f;

    private int rotationNum = 0;// 左回転
    private Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,1,0),
        new Vector3(-1,0,0),
        new Vector3(0,-1,0),
        new Vector3(1,0,0)
    };

    void Start()
    {
        for(int i = 0; i < controllPieces.Length; i++)
            controllPieces[i] = null;
    }

    void Update()
    {
        if (!GameDirector.isWaiting)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 move = new Vector3(0, 0);
        if (Input.GetKeyDown(KeyCode.A))
            move.x = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            move.x = 1;
        // 時間落下
        else if (Time.time - previousTime >= fallTime)
        {
            move.y = -1;

            previousTime = Time.time;

            // S入力すると落ちるスピードアップ
            if (Input.GetKey(KeyCode.S))
            {
                fallTime = (float)0.1;
            }
        }

        Vector3 movedPos = controllPieces[0].transform.position + move;
        Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];
        if (!myMap.CheckWall(movedPos, rotMovedPos))
            return;

        for (int i = 0; i < controllPieces.Length; i++)
            controllPieces[i].transform.Translate(move);

        myMap.GroundStack(movedPos, rotMovedPos);

        // 即置きも作る?
    }

    private void Rotate()
    {
        int lastNum = rotationNum;
        // 左回転
        if (Input.GetKeyDown(KeyCode.Q))
            rotationNum++;
        // 右回転(=左に3回転)
        else if (Input.GetKeyDown(KeyCode.E))
            rotationNum += 3;

        // 疑似回転(移動がややこしくなるのでRotationはいじらない)
        rotationNum %= 4;
        Vector3 deltaRot = controllPieces[0].transform.position + rotationPos[rotationNum];

        if (!myMap.CheckWall(controllPieces[0].transform.position, deltaRot))
        {
            rotationNum = lastNum;
            return;
        }

        controllPieces[1].transform.position = deltaRot;
    }
}
