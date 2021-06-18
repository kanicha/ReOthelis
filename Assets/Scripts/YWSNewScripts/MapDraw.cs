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

	// Start is called before the first frame update
	void Start()
    {
		MapDebug();
		//MinoCheck(6, 6, 2);
		MinoCheck(3, 6, 1);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	//��c�Œu���ꂽ�ꍇ�A���������������̏��ԂŌ������s��

	//����Œu���ꂽ�ꍇ�A����������E�����̏��ԂŌ������s��
	
	//��������ꍇ�A���������������̏��ԂŌ������s��

	//Player 1 = ���v���C���[
	//Player 2 = ���v���C���[
	//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
	
	//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
	
	//�S����������
	private void MinoCheck(int x, int y, int player)
    {
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
						if (player == 2 && map[y + i * s,x + j * s] == white)
						{
							for (int n = 1; n < s; n++)
                            {
								map[y + i * n,x + j * n] = white;
							}
							break;
						}
						else if (player == 1 && map[y + i * s,x + j * s] == black)
                        {
							for (int n = 1; n < s; n++)
                            {
								map[y + i * n, x + j * n] = black;
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
