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
		{ "��", "��", "��","��","�Z","�Z", "��","��", "��", "��" },
		{ "��", "��", "��","��","�Z","�Z", "��","��", "��", "��" },
		{ "��", "��", "�Z","�Z","�Z","��", "�Z","��", "��", "��" },
		{ "��", "��", "�Z","��","��","��", "�Z","��", "��", "��" },
		{ "��", "��", "�Z","�Z","��","��", "��","��", "�Z", "��" },
		{ "��", "�Z", "�Z","��","��","��", "��","�Z", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" }
	};

	const string black = "��";
	const string white = "�Z";
	const string wall = "��";
	const string empty = "��";
	private string color = ""; //����v���C���[�̐F
	private string Opponent = ""; //����v���C���[�̐F
	private string memory_color = "";
	private int blackScore = 0;
	private int whiteScore = 0;

	// Start is called before the first frame update
	void Start()
    {
		MapDebug();
		//MinoCheck(6, 6);
		//MinoCheck(3, 6);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
			map[7, 7] = white;
			map[4, 6] = white;
			Ordering(7, 7, 6, 4, 2);
        }
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
		int scoreCounter = 0;
		//����v���C���[�Ƒ���v���C���[�̐F���擾
		color = map[y,x];
		if (color == black)
		{
			Opponent = white;
		}
		else if (color == white)
        {
			Opponent = black;
        }

		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				Debug.Log("x���� = " + j);
				Debug.Log("y���� = " + i);
				//���������ϐ�
				int Direction_X = x + j;
				int Direction_Y = y + i;

				//�u���ꂽ��̃}�X�͌������Ȃ�
				if (i == 0 && j == 0)
                {
					continue;
                }
				//���������ɑ���v���C���[�̋���݂��Ȃ��ꍇ�A���̕����̌������I��������
				if (map[Direction_Y,Direction_X] != Opponent)
                {
					continue;
                }
				//�����̋����𑫂��Ă���
				for (int s = 2; s < 9; s++)
				{
					Debug.Log("���� = " + s);
					//�����}�X�֐�
					int Range_X = x + j * s;
					int Range_Y = y + i * s;

					if (Range_X >= 0 && Range_X < 10 && Range_Y >= 0 && Range_Y < 9)
					{
						//����̋�𔭌��������Ƃɋ󂫂ɓ��������ꍇ�A���̕����̌������I��������
						if (map[Range_Y, Range_X] == empty || map[Range_Y, Range_X] == wall)
						{
							break; 
						}
						//����̋�𔭌��������Ƃɓ����F�̋�ɓ��������ꍇ�A���̃}�X�ɂ�����܂ł̃}�X�̋���Ђ�����Ԃ�
						if (map[Range_Y, Range_X] == color)
						{
							for (int n = 1; n < s; n++)
                            {
								int Change_X = x + j * n;
								int Change_Y = y + i * n;
								map[Change_Y, Change_X] = color;
								scoreCounter++;
								Debug.Log("count = " + scoreCounter);
							}
							break;
						}
					}
				}
            }
        }
		MapDebug();
		ScoreCount(scoreCounter, color);
    }

	private void MapDebug()
    {
		string printMap = "";
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				printMap += map[i, j].ToString() + ":";
			}
			printMap += "\n";
		}

		Debug.Log(printMap);
	}

	private void ScoreCount(int ChangeAmount, string playerColor)
    {
		if (playerColor == black)
        {
			if (ChangeAmount >= 4)
            {
				blackScore = 150 * ChangeAmount + blackScore;
				Debug.Log("Player1 Score:" + blackScore);
            }
			else
            {
				blackScore = 100 * ChangeAmount + blackScore;
				Debug.Log("Player1 Score:" + blackScore);
			}
        }
		else if (playerColor == white)
        {
			if (ChangeAmount >= 4)
            {
				whiteScore = 150 * ChangeAmount + whiteScore;
				Debug.Log("Player2 Score:" + whiteScore);
			}
			else
            {
				whiteScore = 100 * ChangeAmount + whiteScore;
				Debug.Log("Player2 Score:" + whiteScore);
			}
        }
    }
}
