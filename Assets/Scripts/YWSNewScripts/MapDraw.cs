using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDraw : MonoBehaviour
{
	private const byte _WIDTH = 10;
	private const byte _HEIGHT = 10;
	private string[,] map = new string[_HEIGHT, _WIDTH]
	{
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","�Z","��", "�Z","��", "�Z", "��" },
		{ "��", "��", "�Z","�Z","�Z","��", "��","��", "��", "��" },
		{ "��", "��", "�Z","��","�Z","��", "�Z","��", "�Z", "��" },
		{ "��", "��", "�Z","�Z","�Z","��", "��","��", "�Z", "��" },
		{ "��", "��", "��","��","�Z","��", "�Z","�Z", "�Z", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" }
	};

	const string black = "��";
	const string white = "�Z";
	const string wall = "��";
	const string empty = "��";
	private string color = "";
	string memory_color = "";

	// Start is called before the first frame update
	void Start()
    {
		MapDebug();
		//MinoCheck(6, 6);
		MinoCheck(3, 6);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	//Player 1 = ���v���C���[
	//Player 2 = ���v���C���[
	//��c�Œu���ꂽ�ꍇ�A���������������̏��ԂŌ������s��
	//����Œu���ꂽ�ꍇ�A����������E�����̏��ԂŌ������s��
	//��������ꍇ�A���������������̏��ԂŌ������s��
	public void Ordering(int x1, int y1, int x2, int y2, int player)
    {
		//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
		if (player == 1)
		{
			if (map[y1,x1] == black)
            {
				//��Ԗڂ̋�̐F���L��
				//��Ԗڂ̋��Ԗڂ̋�ɂ���ĂЂ�����Ԃ��ꂽ��A���̋�̂Ђ�����Ԃ菈�����s��Ȃ�
				memory_color = map[y2, x2];
				MinoCheck(x1, y1);
				if (map[y2,x2] == memory_color)
                {
					MinoCheck(x2, y2);
                }
			}
			else if (map[y1,x1] == white)
            {
				if (map[y2,x2] == black)
                {
					memory_color = map[y1, x1];
					MinoCheck(x2, y2);
					if (map[y1,x1] == memory_color)
                    {
						MinoCheck(x1, y1);
                    }
                }
				else if (map[y2,x2] == white)
                {
					MinoCheck(x1, y1);
					MinoCheck(x2, y2);
                }
            }
		}

		//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
		else if (player == 2)
		{
			if (map[y1, x1] == white)
			{
				memory_color = map[y2, x2];
				MinoCheck(x1, y1);
				if (map[y2, x2] == memory_color)
				{
					MinoCheck(x2, y2);
				}
			}
			else if (map[y1, x1] == black)
			{
				if (map[y2, x2] == white)
				{
					memory_color = map[y1, x1];
					MinoCheck(x2, y2);
					if (map[y1, x1] == memory_color)
					{
						MinoCheck(x1, y1);
					}
				}
				else if (map[y2, x2] == white)
				{
					MinoCheck(x1, y1);
					MinoCheck(x2, y2);
				}
			}
		}
    }

	//�S����������
	private void MinoCheck(int x, int y)
    {
		color = map[y,x];
		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				//�u���ꂽ��̃}�X�͌������Ȃ�
				if (i == 0 && j == 0)
                {
					continue;
                }
				//�ǂɓ��������A�܂��͒����Ă��܂��ꍇ�͌������Ȃ�
				if (y + i < 0 || x + j < 0 || y + i > 9 || x + j > 9)
                {
					continue;
                }
				//�����̋����𑫂��Ă���
				for (int s = 2; s < 9; s++)
				{
					if (x + j * s >= 0 && x + j * s < 9 && y + i * s >= 0 && y + i * s < 9)
					{
						//�󂫂ɓ��������ꍇ�A���̕����̌������I��������
						if (map[y + i * s,x + j * s] == empty)
						{
							break; 
						}
						//�����F�̋�ɓ��������ꍇ�A���̃}�X�ɂ�����܂ł̃}�X�̋���Ђ�����Ԃ�
						if (map[y + i * s,x + j * s] == color)
						{
							for (int n = 1; n < s; n++)
                            {
								map[y + i * n,x + j * n] = color;
							}
							break;
						}
					}
				}
            }
        }
		MapDebug();
    }

	private void MapDebug()
    {
		string printMap = "";
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				printMap += map[i, j].ToString() + ":";
			}
			printMap += "\n";
		}

		Debug.Log(printMap);
	}

	
}
