using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;
    [SerializeField]
    private GameObject[] piecePrefab = null;
    [SerializeField]
    private MinoController mc = null;

    private byte generateCount = 0;

    void Start()
    {

        // 最初に全て生成した方がいいかも
    }

    void Update()
    {
        if (mc.isLanding)
        {
            Generate();
            mc.isLanding = false;
        }
    }
    private void Generate()
    {
        for (int i = 0; i < 2; i++)
        {
            // 白黒で雑に生成
            GameObject piece = Instantiate(piecePrefab[i]);
            Vector3 setPosition = root.transform.position + new Vector3(0, i);
            piece.transform.position = setPosition;
            piece.transform.parent = root.transform;
            MinoController.controllPieces[i] = piece;
        }
    }
}

