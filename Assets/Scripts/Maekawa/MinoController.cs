using UnityEngine;

public class MinoController : MonoBehaviour
{
    [SerializeField]
    Map myMap = null;
    public GameObject[] controllPieces = new GameObject[2];

    public bool isLanding = true;

    [SerializeField]
    float fallTime = 1f;

    public int rotationNum = 0;// 左回転
    private Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,  0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0,  0, -1),
        new Vector3(1,  0, 0)
    };

    void Start()
    {

    }

    void Update()
    {
        if (!GameDirector.isGenerate)
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
        else if (Input.GetKeyDown(KeyCode.S))
            move.z = -1;
        // 時間落下
        //else if (Time.time - previousTime >= fallTime)
        //{
        //    move.y = -1;

        //    previousTime = Time.time;

        //    // S入力すると落ちるスピードアップ
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        fallTime = (float)0.1;
        //    }
        //}

        Vector3 movedPos = controllPieces[0].transform.position + move;
        Vector3 rotMovedPos = rotationPos[rotationNum] + movedPos;
        if (!myMap.CheckWall(movedPos) || !myMap.CheckWall(rotMovedPos))
            return;

        controllPieces[0].transform.Translate(move);
        controllPieces[1].transform.Translate(move);

        if (myMap.CheckLanding(movedPos) || myMap.CheckLanding(rotMovedPos))
        {
            myMap.FallPiece(controllPieces[0]);
            myMap.FallPiece(controllPieces[1]);
            GameDirector.isGenerate = true;
        }
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
        Vector3 rotatedPos = controllPieces[0].transform.position + rotationPos[rotationNum];

        if (!myMap.CheckWall(rotatedPos))
        {
            rotationNum = lastNum;
            return;
        }

        controllPieces[1].transform.position = rotatedPos;
    }
}