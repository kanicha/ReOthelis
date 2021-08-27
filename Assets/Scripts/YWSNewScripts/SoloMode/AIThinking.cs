using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThinking : MonoBehaviour
{
    public readonly string empty = "□";
    private int[,] CheckEmpty = new int [8,2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckVertical()
    {
        int ItemNum = 0;
        int InputCoordinate = 0;
        for (int x = 1; x < 9; x++)
        {
            for (int z = 10; z > 2; z--)
            {
                if (Map.Instance.map[z, x] == empty && Map.Instance.map[z+1,x] != empty)
                {
                    CheckEmpty[ItemNum,InputCoordinate] = x;
                    InputCoordinate = 1;
                    CheckEmpty[ItemNum,InputCoordinate] = z;
                    InputCoordinate = 0;
                    ItemNum++;
                    break;
                }
            }
        }
    }

    public void ShowData()
    {
        string printMap = "";
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				printMap += CheckEmpty[i, j].ToString() + ":";
			}
			printMap += "\n";
		}

		Debug.Log(printMap);
    }
}
